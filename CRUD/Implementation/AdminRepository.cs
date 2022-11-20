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
    public class AdminRepository : IAdminRepository
    {
        private EFDBContext context;
        public AdminRepository(EFDBContext _context)
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

        public void DeleteAdmin(Admin adminToDelete)
        {
            context.adminRepo.Remove(adminToDelete);
            context.SaveChanges();
        }
        public Admin GetAdminById(string id)
        {
            return context.adminRepo.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Admin> GetAllAdmins()
        {
            return context.adminRepo.ToList();
        }
        public void SaveAdmin(Admin adminToSave)
        {
            if (GetAdminById(adminToSave.Id) != null)
            {
                DeleteAdmin(GetAdminById(adminToSave.Id));
            }
            context.adminRepo.Add(adminToSave);
            context.SaveChanges();
        }
    }
}
