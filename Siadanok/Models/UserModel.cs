using DataBase.Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Siadanok.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Не указан номер")]
        public string Number { get; set; }
        public byte[]? Picture { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string SecondName { get; set; }
        public List<Item>? ListItems { get; set; }

    }
}