﻿using CRUD;
using DataBase.Entity;
using Siadanok.Models;
using System.Drawing;
using System.Linq;

namespace Siadanok.Services
{
    public class Service
    {
        private List<CartItem> cartItems = new List<CartItem>();
        private DataManager dataManager;
        private readonly ILogger<Service> logger;
        public Service(DataManager dataManager,
                       ILogger<Service> logger)
        {
            this.dataManager = dataManager;
            this.logger = logger;
            //cartItems = new List<CartItem>();
        }
        /*
         *  Convert Logic
         */
        private static byte[] ImageToByteArray(string path)
        {
            Image pic = Image.FromFile(path);
            using (var ms = new MemoryStream())
            {
                pic.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
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
            User user = dataManager.User.GetById(id);
            user.Password = Base64Decode(user.Password);
            return user;
        }
        public UserModel GetUserModel(string userId)
        {
            User user = GetUserById(userId);
            List<CartItem> cartItems = GetAllCartItems().ToList().Where(x=>x.UserId.Equals(userId)).ToList();
            List<Item> items = new List<Item>();
            foreach (CartItem cartItem in cartItems)
            {
                if (cartItem.UserId.Equals(userId))
                {
                    items.Add(GetItemById((int)cartItem.Id));
                }
            }
            return new UserModel() { FirstName=user.FirstName, SecondName=user.SecondName,  Number=user.Number , ListItems = items };
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
        /*
         *  CartItem Logic 
         */
        public IEnumerable<CartItem> GetAllCartItems()
        {
            return dataManager.Cart.GetAllCartItems();
        }
        public void DeleteItem(CartItem cartItemToDelete)
        {
            dataManager.Cart.DeleteItem(cartItemToDelete);
        }
        public CartItem GetCartItemById(int id)
        {
            return dataManager.Cart.GetById(id);
        }
        public void SaveItem(CartItem cartItemToSave)
        {
            dataManager.Cart.SaveItem(cartItemToSave);
        }
        public void SaveItem(string userId, int itemId)
        {
            User user = GetUserById(userId);
            Item item = GetItemById(itemId);
            Console.WriteLine($"userId={0}, itemId={1}",userId, itemId);
            dataManager.Cart.SaveItem(new CartItem() { UserId=user.Id, ItemId=item.Id });
        }
        public void AddCartItem(string userId, int itemId)
        {
            User user = GetUserById(userId);
            Item item = GetItemById(itemId);
            this.cartItems.Add(new CartItem() { UserId=user.Id, ItemId=item.Id});
            logger.LogInformation($"First element of CartItemsList -> {this.cartItems.ElementAt(0).UserId} {this.cartItems.ElementAt(0).ItemId}");
        }
        public void DeleteCartItem(string userId, int itemId)
        {
            CartItem cartItemToDelete = this.cartItems.Find(x => x.UserId.Equals(userId) && x.ItemId == itemId);
            this.cartItems.Remove(cartItemToDelete);
        }
        public void SaveCartItems(string userId)
        {
            List<CartItem> listOfRequired = cartItems.Where(x=>x.UserId.Equals(userId)).ToList();
            foreach (CartItem cartItem in listOfRequired) {
                dataManager.Cart.SaveItem(cartItem);
            }
            cartItems.RemoveAll(x=>x.UserId.Equals(userId));
        }
        public List<Item> GetCartItems(string userId)
        {
            List<CartItem> listOfRequired = cartItems.Where(x => x.UserId.Equals(userId)).ToList();
            List<Item> itemList = new List<Item>();
            foreach (CartItem cartItem in listOfRequired)
            {
                itemList.Add(GetItemById(cartItem.ItemId));
            }
            return itemList;
        }
        public void BuyItems(string userId)
        {
            List<CartItem> listToBuy = cartItems.Where(x => x.UserId.Equals(userId)).ToList();
            cartItems.RemoveAll(x=>x.UserId.Equals(userId));
            foreach(var cartItem in listToBuy)
            {
                SaveItem(cartItem);
            }
        }
    }
}
