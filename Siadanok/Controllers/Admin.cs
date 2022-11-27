using DataBase.Entity;
using DataBase.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Siadanok.Models;
using Siadanok.Services;

namespace Siadanok.Controllers
{
    [Authorize(Roles = "admin")]
    public class Admin : Controller
    {
        private readonly ILogger<Admin> logger;
        private Service service;
        public Admin(ILogger<Admin> logger,
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
        [HttpGet("/Admin/User/{userId}")]
        public ActionResult BanUser(string userId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            ViewBag.UserIdBan = userId;
            return View();
        }
        [HttpPost("/Admin/User/{userId}")]
        public ActionResult BanUser(BanModel banModel)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"BanUser: userId={banModel.UserId}, reason={banModel.Reason}");
            service.DeleteUser(service.GetUserById(banModel.UserId));
            return Redirect("/Admin/User");
        }

        public ActionResult Item()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View(service.GetAllItems().ToList());
        }
        [HttpGet("/Admin/Item/{itemId}")]
        public ActionResult EditItem(int itemId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View(service.GetItemById(itemId));
        }
        [HttpPost("/Admin/Item/{itemId}")]
        public ActionResult EditItem(Item itemToSave)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            IFormFileCollection files = HttpContext.Request.Form.Files;
            foreach (IFormFile file in files)
            {
                logger.LogInformation($"EditItem: id={itemToSave.Id}, Name={itemToSave.Name}" +
                                  $" Type={itemToSave.Type}, IsExotic={itemToSave.IsExotic}");
            }

            itemToSave.Picture = Service.IFormFileToByteArray(files[0]);
            service.SaveItem(itemToSave);
            return Redirect("/Admin/Item");
        }
        [HttpGet("/Admin/Item/add")]
        public ActionResult AddItem()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View();
        }
        [HttpPost("/Admin/Item/add")]
        public ActionResult AddItem(Item itemToSave)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            IFormFileCollection files = HttpContext.Request.Form.Files;
                logger.LogInformation($"AddItem: id={itemToSave.Id}, Name={itemToSave.Name}" +
                      $" Type={itemToSave.Type}, IsExotic={itemToSave.IsExotic}, " +
                      $"Picture={Service.IFormFileToByteArray(files[0])}" +
                      $" Price={itemToSave.Price}");

            itemToSave.Picture = Service.IFormFileToByteArray(files[0]);
            service.SaveItem(itemToSave);
            return Redirect("/Admin/Item");
        }
        [HttpGet("/Admin/Item/{itemId}/delete")]
        public ActionResult DeleteItem(int itemId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"DeleteItem: id={itemId}");
            service.DeleteItem(service.GetItemById(itemId));
            return Redirect("/Admin/Item");
        }

        public ActionResult Manager()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View(service.GetAllManagers().ToList());
        }
        [HttpGet("/Admin/Manager/{managerId}/ban")]
        public ActionResult BanManager(string managerId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            ViewBag.managerId = managerId;
            return View(service.GetAllManagers().ToList());
        }
        [HttpPost("/Admin/Manager/{managerId}/ban")]
        public ActionResult BanManager(BanModel banModel)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"BanManager: Id={banModel.UserId}, Reason={banModel.Reason}");
            service.DeleteManager(service.GetManagerById(banModel.UserId));
            return Redirect("/Admin/Manager");
        }

        [HttpGet("/Admin/Manager/{managerId}")]
        public ActionResult EditManager(string managerId)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            DataBase.Entity.Manager manager = service.GetManagerById(managerId);
            ViewBag.managerId = managerId;
            ViewBag.managerName = manager.Name;
            ViewBag.password = manager.Password;
            ViewBag.department = manager.Department;
            ViewBag.firstname = manager.FirstName;
            ViewBag.secondName = manager.SecondName;

            List< DataBase.Entity.Role > roles = service.GetAllRoles().ToList();
            Role roleToDelete = roles.Find(x=>x.RoleName.Equals("admin"));
            roles.Remove(roleToDelete);

            return View(roles);
        }
        [HttpPost("/Admin/Manager/{managerId}")]
        public ActionResult EditManager(ManagerModel maangerToSave)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"EditItem: Name={maangerToSave.Name}, Password={maangerToSave.Password}, " +
                                  $"Department={maangerToSave.Department}, FirstName={maangerToSave.FirstName}," +
                                  $" SecondName={maangerToSave.SecondName}, Role={maangerToSave.Role}");

            service.SaveManager(new DataBase.Entity.Manager() { Id= maangerToSave.Id, Name= maangerToSave.Name,
                                                                Password=maangerToSave.Password,
                                                                Department= maangerToSave.Department,
                                                                FirstName= maangerToSave.FirstName, SecondName= maangerToSave.SecondName
            });

            UserRole userRole = service.GetAllUserRoles().ToList().Find(x=>x.UserId.Equals(maangerToSave.Id));
            service.DeleteUserRole(userRole);

            service.SaveUserRole(new UserRole() { UserId= maangerToSave.Id, RoleName= maangerToSave.Role});
            return Redirect("/Admin/Manager");
        }
        [HttpGet("/Admin/Manager/add")]
        public ActionResult AddManager()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View();
        }
        [HttpPost("/Admin/Manager/add")]
        public ActionResult AddManager(DataBase.Entity.Manager maangerToSave)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            //id
            logger.LogInformation($"EditItem: Name={maangerToSave.Name}, Password={maangerToSave.Password}, " +
                                  $"Department={maangerToSave.Department}, FirstName={maangerToSave.FirstName}," +
                                  $" SecondName={maangerToSave.SecondName}");

            string guid = Guid.NewGuid().ToString();
            maangerToSave.Id = guid;
            service.SaveManager(maangerToSave);

            service.SaveUserRole(new UserRole() { UserId=guid, RoleName=RoleEnum.manager.ToString()});

            return Redirect("/Admin/Manager");
        }


        public ActionResult Role()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View(service.GetAllRoles());
        }
        [HttpGet("/Admin/Role/add")]
        public ActionResult AddRole()
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            return View();
        }
        [HttpPost("/Admin/Role/add")]
        public ActionResult AddRole(Role roleToSave)
        {
            ViewBag.role = service.GetAllUserRoles().ToList().Find(x => x.UserId.Equals(Request.Cookies["userId"])).RoleName;
            logger.LogInformation($"RoleAdd: id={roleToSave.RoleName}");
            service.SaveItem(roleToSave);
            return Redirect("/Admin/Role");
        }



        // POST: Admin/Create
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
