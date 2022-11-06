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
    public class UserRepository : IUserRepository
    {
        private EFDBContext context;
        public UserRepository(EFDBContext _context)
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
        public IEnumerable<User> GetAllUsers()
        {
            return context.userRepo.ToList();
        }
        public void DeleteUser(User userToDelete)
        {
            context.userRepo.Remove(userToDelete);
            context.SaveChanges();
        }
        public User GetById(string id)
        {
            return context.userRepo.FirstOrDefault(x => x.Id == id);
        }

        public void SaveUser(User userToSave)
        {
            bool check = false;
            while(!check)
            {
                userToSave.Id = Guid.NewGuid().ToString();
                if (GetById(userToSave.Id)==null)
                {
                    userToSave.Password = Base64Encode(userToSave.Password);
                    context.userRepo.Add(userToSave);
                    context.SaveChanges();
                    check = true;   
                }
            }
        }
        public void DropTable()
        {
            
        }
    }
}
