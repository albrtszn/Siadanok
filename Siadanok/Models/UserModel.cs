using DataBase.Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Siadanok.Models
{
    public class UserModel
    {
        public string Number { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public List<Item>? ListItems { get; set; }

    }
}