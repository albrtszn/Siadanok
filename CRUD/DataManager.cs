using CRUD.Implementation;
using CRUD.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    public class DataManager 
    {
        private IItemRepository itemRepository;
        private IUserRepository userRepository;
        private ICartItemRepository cartItemRepository;
        public DataManager(IUserRepository _userRepository, IItemRepository _itemRepository, ICartItemRepository _cartItemRepository)
        {
            this.userRepository = _userRepository;
            this.itemRepository = _itemRepository;
            this.cartItemRepository = _cartItemRepository;
        }
        public IItemRepository Item { get { return itemRepository; } }
        public IUserRepository User { get { return userRepository; } }
        public ICartItemRepository Cart { get { return cartItemRepository; } }
    }
}
