namespace Stationery.Manager
{
    using Stationery.Common.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// IAuthManager
    /// </summary>
    public interface IAuthManager
    {
        #region Methods

        /// <summary>
        /// Checks the login.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="tenant">The tenant.</param>
        /// <returns></returns>
        Task<LoginResponse> CheckLogin(string userName, string password, string tenant);

        /// <summary>
        /// Logouts the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<bool> Logout(string token);

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<RegisterModelResponse> CreateNewUser(RegisterModel model);

        #endregion Methods
    }
}