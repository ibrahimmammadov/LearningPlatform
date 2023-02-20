using System.ComponentModel.DataAnnotations;

namespace Platform.Web.Models
{
    public class SignInInput
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsRemember { get; set; }

    }
}
