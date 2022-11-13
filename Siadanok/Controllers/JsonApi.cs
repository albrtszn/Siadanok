using DataBase.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Siadanok.Models;
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

        public string Register(string number, string password,
                               string firstName, string secondName)
        {
            logger.LogInformation($"Try to register -> number={number}, password={password}, firstName={firstName}, secondName={secondName}");
            if (!number.Equals("") && !password.Equals("") && !firstName.Equals("") && !secondName.Equals(""))
            {
                string userId = Guid.NewGuid().ToString();
                service.SaveUser(new DataBase.Entity.User() { Id = userId, Number = number, Password = password, FirstName = firstName, SecondName = secondName });
                return userId;
            }
            else
                return "unsuccesful register";
        }

        [HttpPost]
        public async Task<string> CartAsync(DeliveryOrderModel deliveryOrderModel)
        {
            string body = "";
            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }
            logger.LogInformation($"body={body}");
            DeliveryOrderModel d = JsonConvert.DeserializeObject<DeliveryOrderModel>(body);
            if (d != null) {
                logger.LogInformation($"Delivery: date={d.Date}, userId={d.UserId}" +
                                      $" cartId={d.CartId}, items={d.Items.Count} " +
                                      $" city={d.City}, street={d.Street}, " +
                                      $"building={d.Building}, apartment={d.Appartment}");
                return "succesful buy";
            }
            else
                return "error while buying";
        }



        public string Menu()
        {
            return JsonConvert.SerializeObject(service.GetAllItems().ToList());
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
