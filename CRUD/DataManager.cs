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
        public DataManager(IUserRepository _userRepository, IItemRepository _itemRepository)
        {
            userRepository = _userRepository;
            itemRepository = _itemRepository;
        }
        public IItemRepository Item { get { return itemRepository; } }
        public IUserRepository User { get { return userRepository; } }
    }
}
