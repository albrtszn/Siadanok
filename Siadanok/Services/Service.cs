using CRUD;
using DataBase.Entity;

namespace Siadanok.Services
{
    public class Service
    {
        private DataManager dataManager;
        private readonly ILogger<Service> logger;
        public Service(DataManager dataManager,
                       ILogger<Service> logger)
        {
            this.dataManager = dataManager;
            this.logger = logger;
        }
        /*
         *  User Logic
         */
        public IEnumerable<User> GetAllUsers()
        {
            return dataManager.User.GetAllUsers();
        }
        public void DeleteUser(User userToDelete)
        {
            dataManager.User.DeleteUser(userToDelete);
        }
        public User GetUserById(string id)
        {
            return dataManager.User.GetById(id);
        }
        public void SaveUser(User userToSave)
        {
            logger.LogInformation($"Saving new user -> {userToSave.FirstName} {userToSave.SecondName}");
            dataManager.User.SaveUser(userToSave);
        }
        /*
         *  Item Logic
         */
        public IEnumerable<Item> GetAllItems()
        {
            return dataManager.Item.GetAllItems();
        }
        public void DeleteItem(Item itemToDelete)
        {
            dataManager.Item.DeleteItem(itemToDelete);
        }
        public Item GetItemById(int id)
        {
            return dataManager.Item.GetById(id);
        }

        public void SaveItem(Item itemToSave)
        {
            dataManager.Item.SaveItem(itemToSave);
        }
    }
}
