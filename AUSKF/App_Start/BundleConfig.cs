using System.Web.Optimization;

namespace AUSKF
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libraries/jquery/jquery-{version}.js",
                        "~/Scripts/libraries/jquery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/libraries/bootstrap/bootstrap.js",
                      "~/Scripts/libraries/respond/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css/flatly").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Styles/bootstrap-flatly.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                  "~/Content/bootstrap.css"));

            // admin
            bundles.Add(new StyleBundle("~/Content/css/admin").Include(
                "~/Areas/Admin/Content/styles/sb-admin.css",
                "~/Areas/Admin/Content/styles/sb-admin",
                 "~/Areas/Admin/Content/styles/plugins/morris.css",
                 "~/Content/font-awesome/css/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/admin-angular").Include(
                "~/Areas/Admin/Scripts/auskf.admin.module.js",
                "~/Areas/Admin/Scripts/admin.layout.controller.js",
                "~/Areas/Admin/Scripts/admin.user.controller.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                //"~/Scripts/libraries/bootstrap/plugins/flot/excanvas.min.js",
                //"~/Scripts/libraries/bootstrap/plugins/flot/flot-data.js",
                //"~/Scripts/libraries/bootstrap/plugins/flot/jquery.flot.js",
                //"~/Scripts/libraries/bootstrap/plugins/flot/jquery.flot.pie.js",
                //"~/Scripts/libraries/bootstrap/plugins/flot/jquery.flot.resize.js",
                //"~/Scripts/libraries/bootstrap/plugins/flot/jquery.flot.tooltip.min.js",
                "~/Scripts/libraries/bootstrap/plugins/morris/morris-data.js",
                "~/Scripts/libraries/bootstrap/plugins/morris/morris.js",
                "~/Scripts/libraries/bootstrap/plugins/morris/raphael.min.js"
            ));

            // Angular 
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/libraries/angular/angular.js",
                "~/Scripts/libraries/angular/angular-route.min.js",
                "~/Scripts/libraries/angular-filter/0.5.4/angular-filter.min.js",
                "~/Scripts/auskf.module.js",
                "~/Scripts/auskf.core.service.js"
                ));

            // News
            bundles.Add(new ScriptBundle("~/bundles/news").Include(
                "~/Areas/News/Scripts/auskf.news.js",
                "~/Areas/News/Scripts/auskf.news.service.js",
                "~/Areas/News/Scripts/auskf.news.controller.js"
             ));

            // AUSKF 
            bundles.Add(new ScriptBundle("~/bundles/auskf").Include(
                "~/Scripts/typings/auskf/auskf.dojos.js",
                "~/Scripts/typings/auskf/auskf.dojos.service.js",
                "~/Scripts/typings/auskf/auskf.dojos.controller.js"
                ));
        }
    }
}
