using DataBase.Entity;
using Microsoft.AspNetCore.Mvc;
using Siadanok.Models;
using Siadanok.Services;
using System.Diagnostics;
using System.Net;

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
            if (Request.Cookies["userId"]!=null)
            {
                ViewBag.message = Request.Cookies["userId"];
            }
            else
            {
                ViewBag.message = "Cookies are empty";
            }
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
            //create a cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTimeOffset.Now.AddHours(12);
            Response.Cookies.Append("userId",userToSave.Id.ToString(), option);
            //HttpCookie userCookies = new HttpCookie();
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