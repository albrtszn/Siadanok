using CRUD;
using CRUD.Implementation;
using DataBase;
using DataBase.Entity;
using Microsoft.AspNetCore.Mvc;
using Siadanok.Models;
using Siadanok.Services;
using System.Diagnostics;

namespace Siadanok.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        //private EFDBContext _context;
        private Service service;
        public HomeController(ILogger<HomeController> logger,
                              Service service)
        {
            this.logger = logger;
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(service);
        }
        [HttpPost]
        public IActionResult Register(User userToSave)
        {
            service.SaveUser(userToSave);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View(service.GetAllItems());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}