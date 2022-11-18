using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Enum;

namespace DataBase.Entity
{
    public class DeliveryOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DeliveryId { get; set; }
        public string DateOfOrder { get; set; }
        public string PayMethod { get; set; }
        public string UserId { get; set; }
        public string CartId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string? Apartment { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; }
    }
}
