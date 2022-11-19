using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Interfaces
{
    public interface IManagerRepository
    {
        IEnumerable<Manager> GetAllManagers();
        Manager GetManagerById(string id);
        void SaveManager(Manager managerToSave);
        void DeleteManager(Manager managerToDelete);
    }
}
