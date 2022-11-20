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
    public class RoleRepository : IRoleRepository
    {
        private EFDBContext context;
        public RoleRepository(EFDBContext _context)
        {
            this.context = _context;
        }
        public void DeleteItem(Role roleToDelete)
        { 
            context.roleRepo.Remove(roleToDelete);
            context.SaveChanges();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return context.roleRepo.ToList();
        }

        public Role GetRoleById(string id)
        {
            return context.roleRepo.FirstOrDefault(x => x.RoleName == id);
        }
        public void DeleteRole(Role roleToDelete)
        {
            context.roleRepo.Remove(roleToDelete);
            context.SaveChanges();
        }

        public void SaveItem(Role roleToSave)
        {
            if (GetRoleById(roleToSave.RoleName) != null)
            {
                DeleteRole(GetRoleById(roleToSave.RoleName));
            }
            context.roleRepo.Add(roleToSave);
            context.SaveChanges();
        }
    }
}
