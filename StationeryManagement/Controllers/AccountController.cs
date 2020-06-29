using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stationery.Common;
using Stationery.Common.Entities;
using Stationery.Common.Enums;
using Stationery.Common.Helpers;
using Stationery.Common.Models;
using Stationery.UI.Helpers;
using Stationery.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Stationery.UI.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    /// <seealso cref="Stationery.UI.Controllers.CommonController" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize]
    public class AccountController : CommonController
    {
        /// <summary>
        /// The door list
        /// </summary>
        private IEnumerable<SelectListItem> roleList;

        /// <summary>
        /// The door list
        /// </summary>
        private IEnumerable<SelectListItem> userList;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="configuration">The configuration.</param>
        public AccountController(IOptions<StationeryAPIOptions> options, IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory, IConfiguration configuration)
            : base(options, httpContextAccessor, loggerFactory, configuration)
        {
        }

        /// <summary>
        /// Gets the door list.
        /// </summary>
        /// <value>
        /// The door list.
        /// </value>
        public IEnumerable<SelectListItem> RoleList
        {
            get
            {
                return this.roleList ?? (this.roleList = this.GetRoles());
            }
        }

        /// <summary>
        /// Gets the door list.
        /// </summary>
        /// <value>
        /// The door list.
        /// </value>
        public IEnumerable<SelectListItem> UserList
        {
            get
            {
                return this.userList ?? (this.userList = this.GetUsers());
            }
        }

        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Logins the specified login view model.
        /// </summary>
        /// <param name="loginViewModel">The login view model.</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var result = await this.PostAsync<JwtResponse>(HttpUriFactory.GetAuthLoginRequest(this.options.Value.APIUrl), loginViewModel);
                if (result != null)
                {
                    string token = Tokens.RefreshToken(result.auth_token);
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                    var claims = jwtToken.Claims;
                    claims = claims.Append(new Claim(Constants.AuthTokenFieldName, result.auth_token));
                    claims = claims.Append(new Claim(Constants.TenantFieldName, loginViewModel.TenantName));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        //Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.Now.AddMinutes(result.expires_in),
                        //The time at which the authentication ticket expires.A
                        // value set here overrides the ExpireTimeSpan option of
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = true,
                        //Whether the authentication session is persisted across
                        // multiple requests.Required when setting the
                        // ExpireTimeSpan option of CookieAuthenticationOptions
                        // set with AddCookie.Also required when setting
                        // ExpiresUtc.

                        IssuedUtc = DateTime.Now,
                        //The time at which the authentication ticket was issued.

                        //RedirectUri = "https://localhost:44341"
                        // The full path or absolute URI to be used as an http
                        // redirect response value.
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return Redirect(loginViewModel.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("All", "Incorrect username/password or account is deactivated");
                    return View(loginViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("All", ex.Message);
                Utilities.LogAppError(this.logger, ex);
                return View(loginViewModel);
            }
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            UserRegisterViewModel model = new UserRegisterViewModel()
            {
                IsActive = true,
                Roles = this.RoleList
            };

            return View(model);
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="file">The file.</param>
        /// <param name="roleSearchString">The role search string.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel user, IFormFile file, string roleSearchString)
        {
            var path = string.Empty;
            try
            {
                bool allValidated = true;
                if (string.IsNullOrEmpty(roleSearchString))
                {
                    ModelState.AddModelError("Roles", "Please select role(s)");
                    allValidated = false;
                }

                // check password
                Regex rgx = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&.])[A-Za-z\d@$!%*#?&.]{6,}$");
                if (!rgx.IsMatch(user.Password))
                {
                    ModelState.AddModelError("Password", "Password requires at least 6 characters, contains digit and non-alphanumeric");
                    allValidated = false;
                }

                if (this.UserList != null && this.UserList.Any(s => s.Text.ToLower() == user.UserName.ToLower()))
                {
                    ModelState.AddModelError("UserName", string.Format("This user {0} has been taken", user.UserName));
                    allValidated = false;
                }

                if (!allValidated)
                {
                    user.Roles = this.RoleList;
                    return View(user);
                }

                roleSearchString = roleSearchString.Remove(roleSearchString.Length - 1);
                RegisterModel model = new RegisterModel()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    DatabaseName = this.dbName,
                    ImageSource = user.ImageSource,
                    PhoneNo = user.PhoneNo,
                    Roles = roleSearchString.Split(",")
                };

                if (file != null)
                {
                    //repare path for file
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\FileUploads\\" + this.tenantName + "\\" + this.httpContext.User.Identity.Name + "\\avatars");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = file.GetFilename();
                    path = Path.Combine(path, fileName);

                    //input image file to system in order load it later
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        model.ImageSource = "FileUploads/" + this.tenantName + "/" + this.httpContext.User.Identity.Name + "/avatars/" + fileName;
                    }
                }

                var addedStatus = await this.PutAsync<RegisterModelResponse>(HttpUriFactory.GetNewUserRequest(this.options.Value.APIUrl), model);
                if (addedStatus != null && addedStatus.AddedResult == LoginResult.Allowed)
                {
                    ViewBag.Message = "Added successful";
                }
                else
                {
                    ViewBag.Message = TempData["Message"];
                }

                user.Roles = this.RoleList;
                return View(user);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                Utilities.LogAppError(this.logger, ex);
                if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                user.Roles = this.RoleList;
                return View(user);
            }
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetRoles()
        {
            var result = this.PostAsync<BaseModel<AppIdentityRole>>(HttpUriFactory.GetRolesRequest(this.options.Value.APIUrl), new RoleRequest()).Result;
            if (result != null)
            {
                return result.Models.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),
                });
            }

            return null;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetUsers()
        {
            var model = this.PostAsync<BaseModel<AppIdentityUser>>(HttpUriFactory.GetUsersRequest(this.options.Value.APIUrl), new UserRequest()).Result;
            if (model != null)
            {
                return model.Models.Select(r => new SelectListItem
                {
                    Text = r.UserName,
                    Value = r.Id.ToString(),
                });
            }

            return null;
        }

    }
}

