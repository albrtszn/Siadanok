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
        private IManagerRepository managerRepository;
        private IAdminRepository adminRepository;
        private ICartItemRepository cartItemRepository;
        private IDeliveryOrderRepository deliveryOrderRepository;
        private IReserveOrderRepository reserveOrderRepository;
        private IRoleRepository roleRepository;
        private IUserRoleRepository userRoleRepository;
        public DataManager(IUserRepository userRepository, IItemRepository itemRepository, ICartItemRepository _cartItemRepository,
                           IDeliveryOrderRepository deliveryOrderRepository, IReserveOrderRepository reserveOrderRepository,
                           IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IManagerRepository managerRepository,
                           IAdminRepository adminRepository)
        {
            this.userRepository = userRepository;
            this.managerRepository = managerRepository;
            this.adminRepository = adminRepository;
            this.itemRepository = itemRepository;
            this.cartItemRepository = _cartItemRepository;
            this.deliveryOrderRepository = deliveryOrderRepository;
            this.reserveOrderRepository = reserveOrderRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
        }
        public IItemRepository Item { get { return itemRepository; } }
        public IUserRepository User { get { return userRepository; } }
        public IManagerRepository Manager { get { return managerRepository; } }
        public IAdminRepository Admin { get { return adminRepository; } }
        public ICartItemRepository Cart { get { return cartItemRepository; } }
        public IDeliveryOrderRepository Delivery { get { return deliveryOrderRepository; } }
        public IReserveOrderRepository Reserve { get { return reserveOrderRepository; } }
        public IRoleRepository Role { get { return roleRepository; } }
        public IUserRoleRepository UserRole { get { return userRoleRepository; } }
    }
}
