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
        private IDeliveryOrderRepository deliveryOrderRepository;
        private IReserveOrderRepository reserveOrderRepository;
        public DataManager(IUserRepository _userRepository, IItemRepository _itemRepository, ICartItemRepository _cartItemRepository,
                           IDeliveryOrderRepository deliveryOrderRepository, IReserveOrderRepository reserveOrderRepository)
        {
            this.userRepository = _userRepository;
            this.itemRepository = _itemRepository;
            this.cartItemRepository = _cartItemRepository;
            this.deliveryOrderRepository = deliveryOrderRepository;
            this.reserveOrderRepository = reserveOrderRepository;
        }
        public IItemRepository Item { get { return itemRepository; } }
        public IUserRepository User { get { return userRepository; } }
        public ICartItemRepository Cart { get { return cartItemRepository; } }
        public IDeliveryOrderRepository Delivery { get { return deliveryOrderRepository; } }
        public IReserveOrderRepository Reserve { get { return reserveOrderRepository; } }
    }
}
