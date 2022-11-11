using DataBase.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siadanok.Services;

namespace Siadanok.Controllers
{
    public class JsonApi : Controller
    {
        private readonly ILogger<HomeController> logger;
        private Service service;
        public JsonApi(Service service,
                       ILogger<HomeController> logger)
        {
            this.service = service;
            this.logger = logger;
        }
        // GET: JsonApi
        public JsonResult Index()
        {
            return Json("Test response!");
        }

        public string Login(string login, string password)
        {
            logger.LogInformation($"Try to login -> login={login}, password={password}");
            User? user = service.GetAllUsers().ToList().Find(x=>x.Number.Equals(login));
            
            if (user != null && password.Equals(user.Password))
                return user.Id;
            else
                return "unsuccesful login";
        }

        // GET: JsonApi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: JsonApi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JsonApi/Create
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

        // GET: JsonApi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JsonApi/Edit/5
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

        // GET: JsonApi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JsonApi/Delete/5
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
