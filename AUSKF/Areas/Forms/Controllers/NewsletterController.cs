namespace AUSKF.Areas.Forms.Controllers
{
    using System;
    using System.Web.Mvc;

    public class NewsletterController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // TODO stick this list in the databse
            return View();
        }

        [HttpGet]
        public ActionResult GetNewsLetter(string newsletter)
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\" + newsletter;
            if (!System.IO.File.Exists(filePath))
            {
                return new HttpNotFoundResult("optional description");

            }
            return File(filePath, "application/pdf");
        }
    }
}