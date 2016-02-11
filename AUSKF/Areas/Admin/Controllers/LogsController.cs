using System.Web.Mvc;

namespace AUSKF.Areas.Admin.Controllers
{
    public class LogsController : Controller
    {
        // GET: Admin/Logs
        public ActionResult Index()
        {
            return View();
        }
    }
}