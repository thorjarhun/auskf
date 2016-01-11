using System.Web.Mvc;

namespace AUSKF.Areas.News
{
    public class NewsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "News";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                name: "News_default",
                url: "News/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new[] { "AUSKF.Areas.News.Controllers" }

            );
        }
    }
}