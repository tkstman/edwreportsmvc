﻿using System.Web;
using System.Web.Optimization;

namespace edwreportsmvc
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
                "~/Scripts/jquery.min.js"
                //,"~/Scripts/jquery-3.3.1.slim.min.js"
                //,"~/Scripts/popper.min.js"
                //,"~/Scripts/bootstrap.js"
                //,"~/Scripts/bootstrap.min.js"
                ,"~/Scripts/bootstrap.bundle.js"
                //,"~/Scripts/bootstrap.bundle.min.js"
                ,"~/Scripts/holder.min.js"
                ,"~/Scripts/main.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/bootstrap.min.css",
                "~/Content/css/pricing.css",
                //"~/Content/css/bootstrap.css",
                "~/Content/css/bootstrap-reboot.min.css"//,
                //"~/Content/css/bootstrap-reboot.css"
                ));
        }
    }
}