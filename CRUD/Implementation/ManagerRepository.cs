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
    public class ManagerRepository : IManagerRepository
    {
        private EFDBContext context;
        public ManagerRepository(EFDBContext _context)
        {
            this.context = _context;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public void DeleteManager(Manager managerToDelete)
        {
            context.managerRepo.Remove(managerToDelete);
            context.SaveChanges();
        }
        public IEnumerable<Manager> GetAllManagers()
        {
            return context.managerRepo.ToList();
        }
        public Manager GetManagerById(string id)
        {
            return context.managerRepo.FirstOrDefault(x => x.Id == id);
        }
        public void SaveManager(Manager managerToSave)
        {
            if (GetManagerById(managerToSave.Id)!=null)
            {
                DeleteManager(GetManagerById(managerToSave.Id));
            }
            context.managerRepo.Add(managerToSave);
            context.SaveChanges();
        }
    }
}
