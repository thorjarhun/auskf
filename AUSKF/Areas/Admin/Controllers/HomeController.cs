namespace AUSKF.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}