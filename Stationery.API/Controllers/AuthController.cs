using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stationery.Common.Enums;
using Stationery.Common.Helpers;
using Stationery.Common.Models;
using Stationery.Manager;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stationery.API.Controllers
{
    /// <summary>
    /// AuthController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    [Authorize]
    public class AuthController : Controller
    {
        /// <summary>
        /// The authentication service
        /// </summary>
        private readonly IAuthManager authManager;
        /// <summary>
        /// The JWT factory
        /// </summary>
        private readonly IJwtFactory jwtFactory;
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController" /> class.
        /// </summary>
        /// <param name="jwtFactory">The JWT factory.</param>
        /// <param name="authManager">The authentication service.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        public AuthController(IJwtFactory jwtFactory, IAuthManager authManager, ILoggerFactory loggerFactory)
        {
            this.jwtFactory = jwtFactory;
            this.authManager = authManager;
            this.logger = loggerFactory.CreateLogger("AuthController");
        }

        /// <summary>
        /// Validates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpGet("validate/{token}")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            ClaimsPrincipal principal = await this.jwtFactory.GetPrincipal(token);
            if (principal == null)
            {
                return BadRequest( "Token invalid");
            }

            return Ok();
        }

        /// <summary>
        /// Logins the specified credentials.
        /// </summary>
        /// <param name="credential">The credentials.</param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel credential)
        {
            try
            {
                var loginStatus = await this.authManager.CheckLogin(credential.Username, credential.Password, credential.TenantName);
                LoginResult result = loginStatus.LoginResult;
                if (result == LoginResult.Allowed)
                {
                    return Ok(await this.jwtFactory.GenerateJwtToken(loginStatus));
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogAppError(ex);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Logouts the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout([FromBody]RefreshTokenModel token)
          => Ok(await this.authManager.Logout(token.Token));

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut("create")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            try
            {
                return Ok(await this.authManager.CreateNewUser(model));
            }
            catch (Exception ex)
            {
                this.logger.LogAppError(ex);
                return BadRequest(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
