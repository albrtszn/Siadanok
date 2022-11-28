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
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View();
        }
        [HttpGet("/Home/accessdenied")]
        public IActionResult Deny()
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User userToSave)
        {
            if (HttpContext.Request.Form.Files.Count != 0) {
                IFormFileCollection files = HttpContext.Request.Form.Files;
                userToSave.Picture = Service.IFormFileToByteArray(files[0]);
            }
            string guid = Guid.NewGuid().ToString();
            userToSave.Id = guid;
            userToSave.Password = Service.Base64Encode(userToSave.Password);
            service.SaveUser(userToSave);

            service.SaveUserRole(new UserRole() { UserId = guid, RoleName = RoleEnum.user.ToString() }); ;

            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(1))
            };

            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleEnum.user.ToString()) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), props);

            CookieOptions option = new CookieOptions();
            option.Expires = DateTimeOffset.Now.AddHours(1);
            Response.Cookies.Append("userId", userToSave.Id.ToString(), option);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl)
        {
            User? user = service.GetAllUsers().ToList().Find(x => x.Number.Equals(loginModel.Login));
            DataBase.Entity.Manager? manager = service.GetAllManagers().ToList().Find(x => x.Name.Equals(loginModel.Login));
            DataBase.Entity.Admin? admin = service.GetAllAdmins().ToList().Find(x => x.Name.Equals(loginModel.Login));

            if (!ModelState.IsValid) {
                ModelState.AddModelError("Login", "error - login");
            }

            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(1))
            };

            if (user != null) {
                Console.WriteLine($"{user.Id},{user.Password}");
                if (Service.Base64Decode(user.Password).Equals(loginModel.Password))
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTimeOffset.Now.AddHours(1);
                    Response.Cookies.Append("userId", user.Id.ToString(), option);
                    //Response.Cookies.Append("cart", null, option);
                    ViewBag.message = Request.Cookies["userId"];
                    ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(user.Id)).RoleName;

                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleEnum.user.ToString()) };
                    // создаем объект ClaimsIdentity
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    // установка аутентификационных куки
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), props);
                }
                return Redirect(returnUrl ?? "/");
            }
            if (manager != null)
            {
                Console.WriteLine($"{manager.Id},{manager.Password}");
                if (manager.Password.Equals(loginModel.Password))
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTimeOffset.Now.AddHours(1);
                    Response.Cookies.Append("userId", manager.Id.ToString(), option);
                    //Response.Cookies.Append("cart", null, option);
                    ViewBag.message = Request.Cookies["userId"];
                    ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(manager.Id)).RoleName;

                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleEnum.manager.ToString()) };
                    // создаем объект ClaimsIdentity
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    // установка аутентификационных куки
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), props);
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
                    ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(admin.Id)).RoleName;

                    var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultRoleClaimType, RoleEnum.admin.ToString()) };
                    // создаем объект ClaimsIdentity
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    // установка аутентификационных куки
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), props);
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
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View();
        }
        public IActionResult Contact()
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View();
        }
        public IActionResult AboutUs()
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View();
        }
        [HttpGet]
        public IActionResult FeedBack()
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View();
        }
        [HttpPost]
        public IActionResult FeedBack(Comment comment)
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            comment.Date = DateTime.Now.ToString("dd.MM.yyyy");
            return View();
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult Menu()
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View(service.GetAllItems());
        }
        [Authorize(Roles="user")]
        [HttpPost]
        public IActionResult Menu(string sort, string filt)
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }

            List<Item> items = service.GetAllItems().ToList();
            logger.LogInformation($"PostMenu: filt={filt}, sort={sort}");

            switch (sort)
            {
                case "up":
                    /*items.Sort(delegate (Item x, Item y) {
                        if (x.Price == null && y.Price == null) return 0;
                        else if (x.Price == null) return -1;
                        else if (y.Price == null) return -1;
                        else return x.Price.CompareTo(y.Price);
                    });*/
                    items = items.OrderByDescending(x => x.Price).Reverse().ToList();
                    break;
                case "down":
                    items = items.OrderByDescending(x=>x.Price).ToList();
                    break;
                default:
                    break;
            }

            switch (filt) {
                case "Meal":
                    var items1  = items.Where(x => x.Type.Equals("Meal"));
                    foreach (var item in items)
                    {
                        logger.LogInformation($"item: Id={item.Id}, Type={item.Type}");
                    }
                return View(items.Where(x => x.Type.Equals("Meal")).ToList());
                    break;

                case "Soup":
                    return View(items.Where(x => x.Type.Equals("Soup")).ToList());
                    break;

                case "Drink":
                    return View(items.Where(x => x.Type.Equals("Drink")).ToList());
                    break;

                case "Dessert":
                    return View(items.Where(x => x.Type.Equals("Dessert")).ToList());
                    break;

                default:
                    return View(items);
                    break;
            }
        }
        //[HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult addCartItem(int itemId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
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
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Order(OrderModel model)
        {
            ViewBag.message = Request.Cookies["userId"];
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
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
                Response.Cookies.Delete("cart");
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
            return Redirect("Index");
        }

        [Authorize(Roles = "user")]
        public IActionResult buyItems()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
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
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
            return View(service.GetUserModel(Request.Cookies["userId"]));
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Cart()
        {
            if (Request.Cookies["userId"] != null)
            {
                ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
                ViewBag.message = Request.Cookies["userId"];
            }
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