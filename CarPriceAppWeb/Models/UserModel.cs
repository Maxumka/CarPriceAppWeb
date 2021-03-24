
using System.ComponentModel.DataAnnotations;

namespace CarPriceAppWeb.Models
{
    public class UserModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Login is too small.")]
        public string Login { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Password is too small.")]
        public string Password { get; set; }
    }
}
