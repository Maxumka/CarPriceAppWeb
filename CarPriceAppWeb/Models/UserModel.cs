using System.ComponentModel.DataAnnotations;

namespace CarPriceAppWeb.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Логин не может быть пустым")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль не может быть пустой")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        public string Password { get; set; }
    }
}
