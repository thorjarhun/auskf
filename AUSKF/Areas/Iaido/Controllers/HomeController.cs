namespace AUSKF.Areas.Iaido.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        // GET: Iaido/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Study()
        {
            return View();
        }

        public ActionResult Points()
        {
            return View();
        }

        public ActionResult History()
        {
            return View();
        }

        public ActionResult ChampionshipResults()
        {
            return View();
        }

        public ActionResult Archives()
        {
            return View();
        }

        public FileResult ShinsaQuestions()
        {
            return File(AppDomain.CurrentDomain.BaseDirectory + 
                "\\Content\\pdfs\\2010_iaido_questions.pdf", "application/pdf");
        }
    }
}