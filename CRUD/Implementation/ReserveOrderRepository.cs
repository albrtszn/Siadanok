using CRUD.Interfaces;
using DataBase;
using DataBase.Entity;
using Siadanok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Implementation
{
    public class ReserveOrderRepository : IReserveOrderRepository
    {
        private EFDBContext context;
        public ReserveOrderRepository(EFDBContext _context)
        {
            this.context = _context;
        }
        public void DeleteItem(ReserveOrder reserveToDelete)
        {
            context.reserveOrderRepo.Remove(reserveToDelete);
            context.SaveChanges();
        }

        public IEnumerable<ReserveOrder> GetAllItems()
        {
            return context.reserveOrderRepo.ToList();
        }

        public ReserveOrder GetById(string id)
        {
            return context.reserveOrderRepo.FirstOrDefault(x => x.ReserveId.Equals(id));
        }

        public void SaveItem(ReserveOrder ReserveOrderToSave)
        {
            context.reserveOrderRepo.Add(ReserveOrderToSave);
            context.SaveChanges();
        }
    }
}
