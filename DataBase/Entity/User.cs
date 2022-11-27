using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required(ErrorMessage = "Введите номер телефона")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Номер должен содержать 13 символов(+375NnXxyyZz)")]
        public string Number { get; set; }
        public byte[]? Picture { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль должен быть больше 7 символов")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Имя должно быть больше 1 символа")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Фамилия должна быть больше 1 символа")]
        public string SecondName { get; set; }

    }
}
