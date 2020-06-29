using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stationery.Common;
using Stationery.Common.Entities;
using Stationery.Common.Enums;
using Stationery.Common.Helpers;
using Stationery.Common.Models;
using Stationery.Data;
using Stationery.Membership.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stationery.Manager
{
    /// <summary>
    /// AuthManager
    /// </summary>
    /// <seealso cref="Stationery.Manager.IAuthManager" />
    /// <seealso cref="RedZone.Membership.API.Services.IAuthService" />
    /// <seealso cref="MSMAuthService.Services.IAuthService" />
    public class AuthManager : IAuthManager
    {
        /// <summary>
        /// The tenant repository
        /// </summary>
        private readonly ITenantRepository tenantRepository;
        /// <summary>
        /// The JWT factory
        /// </summary>
        private readonly IJwtFactory jwtFactory;
        /// <summary>
        /// The user manager
        /// </summary>
        private readonly UserManager<AppIdentityUser> userManager;
        /// <summary>
        /// The sign in manager
        /// </summary>
        private readonly SignInManager<AppIdentityUser> signInManager;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IHttpContextAccessor httpContentAccessor;
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
        /// <summary>
        /// The connection options
        /// </summary>
        private readonly IOptions<ConnectionSettings> connectionOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService" /> class.
        /// </summary>
        /// <param name="jwtFactory">The JWT factory.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="signInManager">The sign in manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="tenantRepository">The tenant repository.</param>
        /// <param name="connectionOptions">The connection options.</param>
        /// <param name="httpContentAccessor">The HTTP content accessor.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public AuthManager(IJwtFactory jwtFactory, UserManager<AppIdentityUser> userManager,
            SignInManager<AppIdentityUser> signInManager,
            ITenantRepository tenantRepository, IOptions<ConnectionSettings> connectionOptions,
            IHttpContextAccessor httpContentAccessor, IUnitOfWork unitOfWork)
        {
            this.jwtFactory = jwtFactory;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tenantRepository = tenantRepository;
            this.unitOfWork = unitOfWork;
            this.httpContentAccessor = httpContentAccessor;
            this.connectionOptions = connectionOptions;
        }

        /// <summary>
        /// Logouts the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<bool> Logout(string token)
        {
            await this.signInManager.SignOutAsync();
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Checks the login.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<LoginResponse> CheckLogin(string userName, string password, string tenant)
        {
            // check user's information
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(tenant))
            {
                return await Task.FromResult(new LoginResponse { LoginResult = LoginResult.NotAllowd });
            }

            // get the tenant info
            var tenantToVerify = await this.tenantRepository.GetSingleAsync(s => s.TanentName == tenant);
            if (tenantToVerify == null)
            {
                return await Task.FromResult(new LoginResponse { LoginResult = LoginResult.NotAllowd });
            }

            using (var context = new MonitorDbContext(MonitorDbContextHelper.ChangeDatabaseNameInConnectionString(this.connectionOptions.Value.DefaultConnection, tenantToVerify.DatabaseName).Options))
            {
                var userHandler = new UserManager<AppIdentityUser>(new AppUserStore(context), Options.Create<IdentityOptions>(this.signInManager.Options),
                    this.userManager.PasswordHasher, this.userManager.UserValidators, this.userManager.PasswordValidators,
                    this.userManager.KeyNormalizer, this.userManager.ErrorDescriber, null, this.userManager.Logger as ILogger<UserManager<AppIdentityUser>>);

                //get the user to verify
                var userToVerify = await userHandler.FindByNameAsync(userName);
                if (userToVerify == null || !userToVerify.IsActive)
                {
                    return await Task.FromResult(new LoginResponse { LoginResult = LoginResult.NotAllowd });
                }

                var singinHandler = new SignInManager<AppIdentityUser>(userHandler, this.httpContentAccessor, this.signInManager.ClaimsFactory,
                    Options.Create<IdentityOptions>(this.signInManager.Options), this.signInManager.Logger as ILogger<SignInManager<AppIdentityUser>>, null);

                // check the credentials
                var pwToVerity = await singinHandler.CheckPasswordSignInAsync(userToVerify, password, false);
                if (!pwToVerity.Succeeded)
                {
                    return await Task.FromResult(new LoginResponse { LoginResult = LoginResult.NotAllowd });
                }

                // get roles claims
                var roles = from ur in context.UserRoles
                            where ur.UserId == userToVerify.Id
                            join r in context.Roles on ur.RoleId equals r.Id
                            select r;

                IEnumerable<Claim> claims = from role in roles
                                            join rc in context.RoleClaims
                                            on role.Id equals rc.RoleId
                                            select new StationeryClaim(rc.ClaimType, rc.ClaimValue, role.IsAdmin);
                bool isAdmin = roles.Any(s => s.IsAdmin == true);
                return new LoginResponse { ImageSource = userToVerify.ImageSource, UserName = userName, IsAdmin = isAdmin, DBName = tenantToVerify.DatabaseName, Claims = claims.ToList(), LoginResult = LoginResult.Allowed };
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<RegisterModelResponse> CreateNewUser(RegisterModel model)
        {
            var user = new AppIdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedDate = DateTime.Now,
                LastModify = DateTime.Now,
                IsActive = model.IsActive,
                ImageSource = model.ImageSource,
                PhoneNumber = model.PhoneNo
            };

            using (var context = new MonitorDbContext(MonitorDbContextHelper.ChangeDatabaseNameInConnectionString(this.connectionOptions.Value.DefaultConnection, model.DatabaseName).Options))
            {
                var userHandler = new UserManager<AppIdentityUser>(new AppUserStore(context), Options.Create<IdentityOptions>(this.signInManager.Options),
                    this.userManager.PasswordHasher, this.userManager.UserValidators, this.userManager.PasswordValidators,
                    this.userManager.KeyNormalizer, this.userManager.ErrorDescriber, null, this.userManager.Logger as ILogger<UserManager<AppIdentityUser>>);

                var result = await userHandler.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await userHandler.AddToRolesAsync(user, model.Roles);
                    return await Task.FromResult(new RegisterModelResponse() { AddedResult = LoginResult.Allowed });
                }

                return await Task.FromResult(new RegisterModelResponse() { AddedResult = LoginResult.NotAllowd, Error = result.Errors.ElementAt(0).Description });
            }
        }
    }
}
