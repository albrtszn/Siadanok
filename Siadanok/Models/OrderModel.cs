using System.ComponentModel.DataAnnotations;

namespace Siadanok.Models
{
    public class OrderModel
    {
        public string? OrderType { get; set; }
        [Required(ErrorMessage = "Выберите метод оплаты")]
        public string? PayMethod { get; set; }
        [Required(ErrorMessage = "Введите город")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Введите улицу")]
        public string? Street { get; set; }
        [Required(ErrorMessage = "Введите номер дома")]
        public string? Building { get; set; }
        public string? Apartment { get; set; }
        public string? Comment { get; set; }


        public string? Table { get; set; }
        [Required(ErrorMessage = "Выберите дату")]
        public DateTime? DateTime { get; set; }
        public string? Wallet { get; set; }
    }
}
