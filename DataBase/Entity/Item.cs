using DataBase.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entity
{
    public class Item
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public string Type { get; set; }
        public string IsExotic { get; set; }
        public string Descryption { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

    }
}
