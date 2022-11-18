using DataBase.Entity;
using Siadanok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Interfaces
{
    public interface IReserveOrderRepository
    {
        IEnumerable<ReserveOrder> GetAllItems();
        ReserveOrder GetById(string id);
        void SaveItem(ReserveOrder ReserveOrderToSave);
        void DeleteItem(ReserveOrder reserveToDelete);
    }
}
