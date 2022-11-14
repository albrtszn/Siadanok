using CRUD.Implementation;
using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Interfaces
{
    public interface IDeliveryOrderRepository
    {
        IEnumerable<DeliveryOrder> GetAllItems();
        DeliveryOrder GetById(string id);
        void SaveItem(DeliveryOrder itemDeliveryOrderToSave);
        void DeleteItem(DeliveryOrder itemToDelete);
    }
}
