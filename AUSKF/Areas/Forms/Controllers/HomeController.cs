namespace AUSKF.Areas.Forms.Controllers
{
    using System;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(string form)
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "Areas\\Forms\\Content\\" + form;
            if (!System.IO.File.Exists(filePath))
            {
                return new HttpNotFoundResult("optional description");

            }
            return File(filePath, "application/pdf");
        }
    }
}