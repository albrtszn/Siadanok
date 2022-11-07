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
        public Item item { get; set; }
        public int count { get; set; }

        public string Cartid { get; set; }
    }
}
