using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Interfaces
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems();
        Item GetById(int id);
        void SaveItem(Item itemToSave);
        void DeleteItem(Item itemToDelete);
    }
}
