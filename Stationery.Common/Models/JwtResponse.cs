namespace Stationery.Common.Models
{
    /// <summary>
    /// JwtResponse
    /// </summary>
    public class JwtResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        /// <value>
        /// The authentication token.
        /// </value>
        public string auth_token
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>
        /// The expires in.
        /// </value>
        public double expires_in
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the jti.
        /// </summary>
        /// <value>
        /// The jti.
        /// </value>
        public string Jti
        {
            get; set;
        }

        #endregion Properties
    }
}