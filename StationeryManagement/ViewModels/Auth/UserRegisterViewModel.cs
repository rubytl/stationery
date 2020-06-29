using System.ComponentModel.DataAnnotations;

namespace Stationery.UI.ViewModels
{
    /// <summary>
    /// UserRegisterViewModel
    /// </summary>
    /// <seealso cref="Stationery.UI.ViewModels.UserViewModel" />
    public class UserRegisterViewModel : UserViewModel
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
