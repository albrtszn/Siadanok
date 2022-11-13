using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entity
{
    public class DeliveryOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id;
        [Required]
        public string Date;
        [Required]
        public string UserId;
        [Required]
        public string CartId;
        [Required]
        public string City;
        [Required]
        public string Street;
        [Required]
        public string Building;
        public string? Apartment;
    }
}
