using System.Web.Mvc;

namespace AUSKF.Areas.News.Controllers
{
    public class HomeController : Controller
    {
        // GET: News/Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: News/Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: News/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: News/Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: News/Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: News/Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
