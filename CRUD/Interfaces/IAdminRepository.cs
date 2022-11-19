using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> GetAllAdmins();
        Admin GetAdminById(string id);
        void SaveAdmin(Admin adminToSave);
        void DeleteAdmin(Admin adminToDelete);
    }
}
