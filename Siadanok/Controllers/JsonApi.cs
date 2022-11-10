using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Siadanok.Controllers
{
    public class JsonApi : Controller
    {
        // GET: JsonApi
        public JsonResult Index()
        {
            return Json("Test response!");
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
