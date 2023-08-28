using System.ComponentModel.DataAnnotations;

namespace ReadAddict.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid EmailAddress")]
        public string Email { get; set; } = string.Empty;


        [Required]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
