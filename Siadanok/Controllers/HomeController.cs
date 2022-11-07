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
            //Response.Cookies.Append("SessionId", Guid.NewGuid().ToString());
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.message = Request.Cookies["userId"];
            }
            else
            {
                ViewBag.message = "Cookies are empty";
            }

            /*if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionId")))
            {
                HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString()); ;
            }
            var name = HttpContext.Session.GetString("SessionId");

            logger.LogInformation("Session Name: {Name}", name);*/
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User userToSave)
        {
            service.SaveUser(userToSave);
            //create a cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTimeOffset.Now.AddHours(12);
            Response.Cookies.Append("userId", userToSave.Id.ToString(), option);
            //HttpCookie userCookies = new HttpCookie();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Menu()
        {
            ViewBag.message = Request.Cookies["userId"];
            return View(service.GetAllItems());
        }
        [HttpPost]
        public IActionResult Menu(int itemId)
        {
            if (Request.Cookies["SessionId"] == null)
            {
                //ViewBag.message = Request.Cookies["userId"];
                CookieOptions option = new CookieOptions();
                option.Expires = DateTimeOffset.Now.AddHours(1);
                Response.Cookies.Append("SessionId", Guid.NewGuid().ToString(), option);
            }
             ViewBag.message = Request.Cookies["userId"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}