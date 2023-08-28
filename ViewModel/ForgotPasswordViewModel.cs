using System.ComponentModel.DataAnnotations;

namespace ReadAddict.ViewModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Invalid EmailAddress")]
        public string Email { get; set; } = string.Empty;

    }
}
