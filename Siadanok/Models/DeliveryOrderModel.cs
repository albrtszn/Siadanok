using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataBase.Entity;

namespace Siadanok.Models
{
    public class DeliveryOrderModel
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string CartId { get; set; }
        public List<Item> Items { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string? Appartment { get; set; }
    }
}
