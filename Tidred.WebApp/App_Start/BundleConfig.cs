using System.Web;
using System.Web.Optimization;

namespace Tidred.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jQueryOverrides.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/datepicker3.css"));

            bundles.Add(new ScriptBundle("~/bundles/tidred").Include(
                      "~/Scripts/tidred/account.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                      "~/Scripts/angular.js",
                      "~/Scripts/angular-route.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-ui").Include(
                      //"~/Scripts/angular-ui/ui-bootstrap.js",
                      "~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(new ScriptBundle("~/bundles/appAdmin").Include(
                      "~/appAdmin/adminApp.js",
                      "~/appAdmin/adminService.js",
                      "~/appAdmin/adminController.js",
                      "~/appAdmin/customerController.js",
                      "~/appAdmin/customerEditController.js",
                      "~/appAdmin/projectController.js",
                      "~/appAdmin/projectEditController.js",
                      "~/appAdmin/userController.js",
                      "~/appAdmin/userCreateController.js"));

            bundles.Add(new ScriptBundle("~/bundles/appTime").Include(
                    "~/appTime/timeApp.js",
                    "~/appTime/timeService.js",
                    "~/appTime/timeController.js",
                    "~/appTime/timeRecordController.js",
                    "~/appTime/timeRecordEditController.js"));


        }
    }
}
