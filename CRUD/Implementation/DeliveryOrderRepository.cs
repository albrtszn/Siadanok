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
    public class DeliveryOrderRepository : IDeliveryOrderRepository
    {
        private EFDBContext context;
        public DeliveryOrderRepository(EFDBContext _context)
        {
            this.context = _context;
        }
        public void DeleteItem(DeliveryOrder itemDeliveryOrderToDelete)
        {
            context.deliveryOrderRepo.Remove(itemDeliveryOrderToDelete);
            context.SaveChanges();
        }

        public IEnumerable<DeliveryOrder> GetAllItems()
        {
            return context.deliveryOrderRepo.ToList();
        }

        public DeliveryOrder GetById(string id)
        {
            return context.deliveryOrderRepo.FirstOrDefault(x => x.OrderId.Equals(id));
        }

        public void SaveItem(DeliveryOrder itemDeliveryOrderToSave)
        {
            context.deliveryOrderRepo.Add(itemDeliveryOrderToSave);
            context.SaveChanges();
        }
    }
}
