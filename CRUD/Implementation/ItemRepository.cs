using CRUD.Interfaces;
using DataBase;
using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Implementation
{
    public class ItemRepository : IItemRepository
    {
        private EFDBContext context;
        public ItemRepository(EFDBContext _context)
        {
            this.context = _context;
        }
        public IEnumerable<Item> GetAllItems()
        {
            return context.itemRepo.ToList();
        }
        public void DeleteItem(Item itemToDelete)
        {
            context.itemRepo.Remove(itemToDelete);
            context.SaveChanges();
        }
        public Item GetById(int id)
        {
            return context.itemRepo.FirstOrDefault(x => x.Id == id);
        }

        public void SaveItem(Item itemToSave)
        {
            context.itemRepo.Add(itemToSave);
            context.SaveChanges();
        }
        public void DropTable()
        {

        }
    }
}
