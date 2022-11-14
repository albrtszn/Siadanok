using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entity
{
    public class CartItem
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }
    }
}
