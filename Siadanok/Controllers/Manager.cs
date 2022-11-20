using DataBase.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siadanok.Models;
using Siadanok.Services;

namespace Siadanok.Controllers
{
    [Authorize(Roles = "manager")]
    public class Manager : Controller
    {
        private readonly ILogger<Manager> logger;
        private Service service;
        public Manager(ILogger<Manager> logger,
                              Service service)
        {
            this.logger = logger;
            this.service = service;
        }
        // GET: ManagerController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult User()
        {
            return View(service.GetAllUsers().ToList());
        }
        [HttpGet("/Manager/User/{userId}")]
        public ActionResult BanUser(string userId)
        {
            ViewBag.UserIdBan = userId;
            return View();
        }
        [HttpPost("/Manager/User/{userId}")]
        public ActionResult BanUser(BanModel banModel)
        {
            logger.LogInformation($"BanUser: userId={banModel.UserId}, reason={banModel.Reason}");
            service.DeleteUser(service.GetUserById(banModel.UserId));
            return Redirect("/Manager/User");
        }

        public ActionResult Item()
        {
            return View(service.GetAllItems().ToList());
        }
        [HttpGet("/Manager/Item/{itemId}")]
        public ActionResult EditItem(int itemId)
        {
            return View(service.GetItemById(itemId));
        }
        [HttpPost("/Manager/Item/{itemId}")]
        public ActionResult EditItem(Item itemToSave)
        {
            //!!!
            logger.LogInformation($"EditItem: id={itemToSave.Id}, Name={itemToSave.Name}" +
                                  $" Type={itemToSave.Type}, IsExotic={itemToSave.IsExotic}");
            service.SaveItem(itemToSave);
            return Redirect("/Manager/Item");
        }

        [HttpGet("/Manager/Item/add")]
        public ActionResult AddItem()
        {
            return View();
        }
        [HttpPost("/Manager/Item/add")]
        public ActionResult AddItem(Item itemToSave)
        {
            logger.LogInformation($"EditItem: id={itemToSave.Id}, Name={itemToSave.Name}" +
                                  $" Type={itemToSave.Type}, IsExotic={itemToSave.IsExotic}");
            service.SaveItem(itemToSave);
            return Redirect("/Manager/Item");
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
