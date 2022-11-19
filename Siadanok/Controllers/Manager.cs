using DataBase.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siadanok.Services;

namespace Siadanok.Controllers
{
    public class Manager : Controller
    {
        private readonly ILogger<HomeController> logger;
        private Service service;
        public Manager(ILogger<HomeController> logger,
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
            logger.LogInformation($"EditItem: id={itemToSave.Id}, Name={itemToSave.Name}" +
                                  $" Type={itemToSave.Type}, IsExotic={itemToSave.IsExotic}");
            return Redirect("/Manager/Item");
        }


        // GET: ManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ManagerController/Create
        public ActionResult Create()
        {
            return View();
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

        // GET: ManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
