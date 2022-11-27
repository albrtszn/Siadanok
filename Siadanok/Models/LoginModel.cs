using System.ComponentModel.DataAnnotations;

namespace Siadanok.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите номер телефона")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string? Password { get; set; }
    }
}
