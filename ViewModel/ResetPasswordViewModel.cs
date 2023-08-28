using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ReadAddict.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set;} = string.Empty;
    }
}
