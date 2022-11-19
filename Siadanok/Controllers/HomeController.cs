using DataBase.Entity;
using DataBase.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Siadanok.Models;
using Siadanok.Services;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;

namespace Siadanok.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private Service service;
        public HomeController(ILogger<HomeController> logger,
                              Service service)
        {
            this.logger = logger;
            this.service = service;
        }

        public IActionResult Index()
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.message = Request.Cookies["userId"];
            }
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
            option.Expires = DateTimeOffset.Now.AddHours(1);
            Response.Cookies.Append("userId", userToSave.Id.ToString(), option);
            //Response.Cookies.Append("cart", "", option);
            //HttpCookie userCookies = new HttpCookie();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl)
        {
            User? user = service.GetAllUsers().ToList().Find(x => x.Number.Equals(loginModel.Login));
            DataBase.Entity.Manager? manager = service.GetAllManagers().ToList().Find(x => x.Name.Equals(loginModel.Login));
            Admin? admin = service.GetAllAdmins().ToList().Find(x => x.Name.Equals(loginModel.Login));

            if (user != null) {
                Console.WriteLine($"{user.Id},{user.Password}");
                if (Service.Base64Decode(user.Password).Equals(loginModel.Password))
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTimeOffset.Now.AddHours(1);
                    Response.Cookies.Append("userId", user.Id.ToString(), option);
                    //Response.Cookies.Append("cart", null, option);
                    ViewBag.message = Request.Cookies["userId"];

                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleEnum.user.ToString()) };
                    // создаем объект ClaimsIdentity
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    // установка аутентификационных куки
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                }
                return Redirect(returnUrl ?? "/");
            }
            if (manager != null)
            {
                Console.WriteLine($"{manager.Id},{manager.Password}");
                if (Service.Base64Decode(manager.Password).Equals(loginModel.Password))
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTimeOffset.Now.AddHours(1);
                    Response.Cookies.Append("userId", manager.Id.ToString(), option);
                    //Response.Cookies.Append("cart", null, option);
                    ViewBag.message = Request.Cookies["userId"];

                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleEnum.manager.ToString()) };
                    // создаем объект ClaimsIdentity
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    // установка аутентификационных куки
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                }
                return Redirect(returnUrl ?? "/");
            }
            if (admin != null)
            {
                Console.WriteLine($"{admin.Id},{admin.Password}");
                if (Service.Base64Decode(admin.Password).Equals(loginModel.Password))
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTimeOffset.Now.AddHours(1);
                    Response.Cookies.Append("userId", admin.Id.ToString(), option);
                    //Response.Cookies.Append("cart", null, option);
                    ViewBag.message = Request.Cookies["userId"];

                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleEnum.admin.ToString()) };
                    // создаем объект ClaimsIdentity
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    // установка аутентификационных куки
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                }
                return Redirect(returnUrl ?? "/");
            }
            return Content("incorrect data");

        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("userId");
            Response.Cookies.Delete("cart");
            return Redirect("/");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult Menu()
        {
            ViewBag.message = Request.Cookies["userId"];
            return View(service.GetAllItems());
        }
        [Authorize(Roles = "manager")]
        public string manager()
        {
            return "manager";
        }
        [Authorize(Roles = "admin")]
        public string admin()
        {
            return "admin";
        }
        //[HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult addCartItem(int itemId)
        {
            if (Request.Cookies["cart"] == null)
            {
                List<CartItem> listCart = new List<CartItem>();
                listCart.Add(new CartItem() { ItemId = itemId, UserId = Request.Cookies["userId"] });
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                List<CartItem> listCart = JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["cart"]);
                listCart.Add(new CartItem() { ItemId = itemId, UserId = Request.Cookies["userId"] });
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(listCart));
            }
            return Redirect("Menu");
        }
        [Authorize(Roles = "user")]
        public IActionResult deleteCartItem(int itemId)
        {
            List<CartItem> listCart = JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["cart"]);
            if (listCart.Count == 1)
            {
                Response.Cookies.Delete("cart");
            }
            else
            {
                CartItem cartItemToDelete = listCart.First(x => x.ItemId.Equals(itemId));
                listCart.Remove(cartItemToDelete);
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(listCart));
            }
            return Redirect("Cart");
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Order()
        {
            ViewBag.message = Request.Cookies["userId"];
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Order(OrderModel model)
        {
            ViewBag.message = Request.Cookies["userId"];
            logger.LogInformation($"New Order -> orderType={model.OrderType}, payMeythd={model.PayMethod}, city={model.City}, street={model.Street}," +
                                  $" building={model.Building}, appartment={model.Apartment}, comment={model.Comment}" +
                                  $" Table={model.Table}, DateTime={model.DateTime}");
            List<CartItem> listCart = JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["cart"]);
            if (model.OrderType.Equals("Delivery"))
            {
                string cartId = Guid.NewGuid().ToString();
                DeliveryOrder delivery = new DeliveryOrder() {
                    DeliveryId = Guid.NewGuid().ToString(),
                    DateOfOrder = DateTime.Now.ToString(),
                    PayMethod = model.PayMethod,
                    UserId = Request.Cookies["userId"],
                    CartId = cartId,
                    City = model.City,
                    Street = model.Street,
                    Building = model.Building,
                    Apartment = model.Apartment,
                    Comment = model.Comment,
                    Status = StatusOfDelivery.Created.ToString()
                };
                service.SaveItem(delivery);
                foreach (CartItem cartItem in listCart)
                {
                    cartItem.CartId = cartId;
                    service.SaveItem(cartItem);
                }
            }
            if (model.OrderType.Equals("Reserve"))
            {
                string cartId = Guid.NewGuid().ToString();
                ReserveOrder reserve = new ReserveOrder()
                {
                    ReserveId = Guid.NewGuid().ToString(),
                    DateOfOrder = DateTime.Now.ToString(),
                    PayMethod = model.PayMethod,
                    UserId = Request.Cookies["userId"],
                    ReserveDate = model.DateTime.ToString(),
                    CartId = cartId,
                    Table = model.Table
                };
                service.SaveItem(reserve);
                foreach (CartItem cartItem in listCart)
                {
                    cartItem.CartId = cartId;
                    service.SaveItem(cartItem);
                }
                Response.Cookies.Delete("cart");
            }
            return View();
        }

        [Authorize(Roles = "user")]
        public IActionResult buyItems()
        {
            List<CartItem> listCart = JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["cart"]);
            string cartId = Guid.NewGuid().ToString();
            foreach (CartItem cartItem in listCart)
            {
                cartItem.CartId = cartId;
                service.SaveItem(cartItem);
            }
            Response.Cookies.Delete("cart");
            return Redirect("/");
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Account()
        {
            ViewBag.message = Request.Cookies["userId"];
            return View(service.GetUserModel(Request.Cookies["userId"]));
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Cart()
        {
            ViewBag.message = Request.Cookies["userId"];
            if (Request.Cookies["cart"] != null) {
                
                List<CartItem>? listCart = JsonConvert.DeserializeObject<List<CartItem>>(Request.Cookies["cart"]);
                return View(service.GetItemsByCartItems(listCart));
            }
            else
                return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}