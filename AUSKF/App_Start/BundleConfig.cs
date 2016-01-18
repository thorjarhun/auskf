using System.Web.Optimization;

namespace AUSKF
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css/flatly").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Styles/bootstrap-flatly.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include(
                  "~/Content/bootstrap.css"));

            // admin
            bundles.Add(new StyleBundle("~/Content/css/admin").Include(
                      "~/Areas/Admin/Content/styles/dist/css/AdminLTE.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/all-skins.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-black-light.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-black.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-blue-light.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-blue.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-green-light.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-green.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-purple-light.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-purple.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-red-light.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-red.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-yellow-light.css",
                      "~/Areas/Admin/Content/styles/dist/css/skins/skin-yellow.css"));

            // Admin 
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Areas/Admin/Scripts/slimScroll/jquery-slimscroll.js",
                 "~/Areas/Admin/Scripts/fastclick/fastclick.js",
                 "~/Areas/Admin/Scripts/app.js",
                 "~/Areas/Admin/Scripts/demo.js"
                ));

            // Admin dashboard
            bundles.Add(new ScriptBundle("~/bundles/admin/dashboard").Include(
                "~/Areas/Admin/Scripts/pages/dashboard.js"
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
