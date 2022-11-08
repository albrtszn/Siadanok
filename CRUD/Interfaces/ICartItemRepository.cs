using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Interfaces
{
    public interface ICartItemRepository
    {
        IEnumerable<CartItem> GetAllCartItems();
        CartItem GetById(int id);
        void SaveItem(CartItem itemToSave);
        void DeleteItem(CartItem itemToDelete);
    }
}
