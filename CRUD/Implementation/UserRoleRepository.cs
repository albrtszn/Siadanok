using DataBase.Entity;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.Interfaces;

namespace CRUD.Implementation
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private EFDBContext context;
        public UserRoleRepository(EFDBContext _context)
        {
            this.context = _context;
        }
        public void DeleteItem(UserRole userRoleToDelete)
        {
            context.userRoleRepo.Remove(userRoleToDelete);
            context.SaveChanges();
        }

        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return context.userRoleRepo.ToList();
        }

        public UserRole GetUserRoleById(int id)
        {
            return context.userRoleRepo.FirstOrDefault(x => x.Id == id);
        }

        public void SaveItem(UserRole userRoleToSave)
        {
            context.userRoleRepo.Add(userRoleToSave);
            context.SaveChanges();
        }
    }
}
