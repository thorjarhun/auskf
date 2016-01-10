using System.Web.Mvc;

namespace AUSKF.Controllers
{
    public class DojoListController : Controller
    {
        // GET: Dojo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dojo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dojo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dojo/Create
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

        // GET: Dojo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dojo/Edit/5
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

        // GET: Dojo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dojo/Delete/5
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
