using Stationery.Common.Enums;
using System.Collections.Generic;

namespace Stationery.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the tenant database.
        /// </summary>
        /// <value>
        /// The tenant database.
        /// </value>
        public string TenantDB
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the tenant.
        /// </summary>
        /// <value>
        /// The name of the tenant.
        /// </value>
        public string TenantName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get; set;
        }

        #endregion Properties
    }

    public class RefreshTokenModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token
        {
            get; set;
        }

        #endregion Properties
    }

    /// <summary>
    /// RegisterModel
    /// </summary>
    public class RegisterModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the phone no.
        /// </summary>
        /// <value>
        /// The phone no.
        /// </value>
        public string PhoneNo { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>
        /// The image source.
        /// </value>
        public string ImageSource { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserDetailViewModel" /> is locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if locked; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IEnumerable<string> Roles { get; set; }
        #endregion Properties
    }

    /// <summary>
    /// 
    /// </summary>
    public class RegisterModelResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the check login result.
        /// </summary>
        /// <value>
        /// The check login result.
        /// </value>
        public LoginResult AddedResult { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }

        #endregion Properties
    }

}