using System.Web.Mvc;

namespace AUSKF.Areas.Officers
{
    public class OfficersAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Officers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "Officers_default",
                url: "Officers/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AUSKF.Areas.Officers.Controllers" }
            );
        }
    }
}