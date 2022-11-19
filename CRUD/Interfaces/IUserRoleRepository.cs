using DataBase.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Interfaces
{
    public interface IUserRoleRepository
    {
        IEnumerable<UserRole> GetAllUserRoles();
        UserRole GetUserRoleById(int id);
        void SaveItem(UserRole userRoleToSave);
        void DeleteItem(UserRole userRoleToDelete);
    }
}
