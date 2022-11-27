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
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View();
        }
        public ActionResult User()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View(service.GetAllUsers().ToList());
        }
        [HttpGet("/Manager/User/{userId}")]
        public ActionResult BanUser(string userId)
        {
            ViewBag.UserIdBan = userId;
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View();
        }
        [HttpPost("/Manager/User/{userId}")]
        public ActionResult BanUser(BanModel banModel)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"BanUser: userId={banModel.UserId}, reason={banModel.Reason}");
            service.DeleteUser(service.GetUserById(banModel.UserId));
            return Redirect("/Manager/User");
        }

        public ActionResult Item()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View(service.GetAllItems().ToList());
        }
        [HttpGet("/Manager/Item/{itemId}")]
        public ActionResult EditItem(int itemId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View(service.GetItemById(itemId));
        }
        [HttpPost("/Manager/Item/{itemId}")]
        public ActionResult EditItem(Item itemToSave)
        {
            //!!!
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"EditItem: id={itemToSave.Id}, Name={itemToSave.Name}" +
                                  $" Type={itemToSave.Type}, IsExotic={itemToSave.IsExotic}");
            IFormFileCollection files = HttpContext.Request.Form.Files;
            if (itemToSave.Picture!=null)
            {
                itemToSave.Picture = Service.IFormFileToByteArray(files[0]);
                service.SaveItem(itemToSave);
            }
            else
            {
                itemToSave.Picture = service.GetItemById(itemToSave.Id).Picture;
                service.SaveItem(itemToSave);
            }
            return Redirect("/Manager/Item");
        }
        [HttpPost("/Manager/Item/{itemId}/delete")]
        public ActionResult DeleteItem(int itemId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"DeleteItem: id={itemId}");
            service.DeleteItem(service.GetItemById(itemId));
            return Redirect("/Admin/Item");
        }

        [HttpGet("/Manager/Item/add")]
        public ActionResult AddItem()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View();
        }
        [HttpPost("/Manager/Item/add")]
        public ActionResult AddItem(Item itemToSave)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"EditItem: id={itemToSave.Id}, Name={itemToSave.Name}" +
                                  $" Type={itemToSave.Type}, IsExotic={itemToSave.IsExotic}");
            IFormFileCollection files = HttpContext.Request.Form.Files;
            itemToSave.Picture = Service.IFormFileToByteArray(files[0]);
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
