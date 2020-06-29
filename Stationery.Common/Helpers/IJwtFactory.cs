/// <summary>
/// 
/// </summary>
namespace Stationery.Common.Helpers
{
    using Stationery.Common.Models;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface IJwtFactory
    {
        #region Methods

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="tanentId">The tanent identifier.</param>
        /// <returns></returns>
        Task<JwtResponse> GenerateJwtToken(LoginResponse loginResponse);

        /// <summary>
        /// Gets the principal.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<ClaimsPrincipal> GetPrincipal(string token);

        #endregion Methods
    }
}