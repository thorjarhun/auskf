using System.Web.Mvc;

namespace AUSKF.Areas.Iaido
{
    public class IaidoAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Iaido";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "Iaido_default",
                url: "Iaido/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "AUSKF.Areas.Iaido.Controllers" }
            );
        }
    }
}