using System.Web.Optimization;

namespace App.Schedule.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.UseCdn = true;
            //var materialCDNFont = "https://fonts.googleapis.com/icon?family=Material+Icons";

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/no-border.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar/jquery").Include(
                   "~/Scripts/moment.js",
                   "~/Scripts/fullcalendar*"));

            bundles.Add(new StyleBundle("~/bundles/fullcalendar/css").Include(
                "~/Content/fullcalendar.css"));

        }
    }
}
