using System.Web.Mvc;

namespace AUSKF.Areas.Forms
{
    public class FormsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Forms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "Forms_default",
                url: "Forms/{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
               namespaces: new[] { "AUSKF.Areas.Forms.Controllers" }
           );
        }
    }
}