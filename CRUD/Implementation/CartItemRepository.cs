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
    public class CartItemRepository : ICartItemRepository
    {
        private EFDBContext context;
        public CartItemRepository(EFDBContext _context)
        {
            this.context = _context;
        }
        public IEnumerable<CartItem> GetAllCartItems()
        {
            return context.cartItemsRepo.ToList();
        }
        public void DeleteItem(CartItem cartItemToDelete)
        {
            context.cartItemsRepo.Remove(cartItemToDelete);
            context.SaveChanges();
        }
        public CartItem GetById(int id)
        {
            return context.cartItemsRepo.FirstOrDefault(x => x.Id == id);
        }

        public void SaveItem(CartItem cartItemToSave)
        {
            context.cartItemsRepo.Add(cartItemToSave);
            context.SaveChanges();
        }
    }
}
