using System.Web.Mvc;

namespace AUSKF.Areas.Admin.Controllers
{
    public class EventsController : Controller
    {
        // GET: Admin/Events
        public ActionResult Index()
        {
            return View();
        }
    }
}