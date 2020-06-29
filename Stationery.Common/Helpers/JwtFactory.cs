using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Stationery.Common.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stationery.Common.Helpers
{
    /// <summary>
    /// JwtFactory
    /// </summary>
    /// <seealso cref="Stationery.Common.Helpers.IJwtFactory" />
    public class JwtFactory : IJwtFactory
    {
        /// <summary>
        /// The JWT options
        /// </summary>
        private readonly JwtIssuerOptions jwtOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtFactory" /> class.
        /// </summary>
        /// <param name="jwtOptions">The JWT options.</param>
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
            this.ThrowIfInvalidOptions(this.jwtOptions);
        }

        /// <summary>
        /// Throws if invalid options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">
        /// options
        /// or
        /// SigningCredentials
        /// or
        /// JtiGenerator
        /// </exception>
        /// <exception cref="ArgumentException">Must be a non-zero TimeSpan. - ValidFor</exception>
        /// <exception cref="System.ArgumentNullException">options
        /// or
        /// SigningCredentials
        /// or
        /// JtiGenerator</exception>
        /// <exception cref="System.ArgumentException">Must be a non-zero TimeSpan. - ValidFor</exception>
        private void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="loginResponse"></param>
        /// <returns></returns>
        public async Task<JwtResponse> GenerateJwtToken(LoginResponse loginResponse)
        {
            string jti = await this.jwtOptions.JtiGenerator();
            var newClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginResponse.UserName),
                new Claim(Constants.DatabaseFieldName, loginResponse.DBName),
                new Claim(Constants.ImageSourceName, loginResponse.ImageSource!=null?loginResponse.ImageSource:string.Empty)
            };

            if (loginResponse.IsAdmin.HasValue && loginResponse.IsAdmin.Value)
            {
                newClaims.Add(new Claim(ClaimTypes.Role, Constants.AdminFieldName));
            }

            var claims = newClaims.Concat(loginResponse.Claims);
            var jwt = new JwtSecurityToken(
                issuer: this.jwtOptions.Issuer,
                audience: this.jwtOptions.Audience,
                claims: claims,
                notBefore: this.jwtOptions.NotBefore,
                expires: this.jwtOptions.Expiration,
                signingCredentials: this.jwtOptions.SigningCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new JwtResponse()
            {
                auth_token = token,
                expires_in = jwtOptions.ValidFor.TotalMinutes,
                Jti = jti
            };

            return await Task.FromResult(response);
        }

        /// <summary>
        /// Gets the principal.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<ClaimsPrincipal> GetPrincipal(string token)
        {
            try
            {
                token = Tokens.RefreshToken(token);
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = false,
                    IssuerSigningKey = this.jwtOptions.SecurityKey
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return await Task.FromResult(principal);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
