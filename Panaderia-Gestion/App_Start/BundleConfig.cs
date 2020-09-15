using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Panaderia_Gestion
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(
                "~/Content/vendor/fontawesome-free/css/all.min.css",
                "~/Content/css/sb-admin-2.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include(
                "~/Content/vendor/jquery/jquery.min.js",
                "~/Content/vendor/jquery-easing/jquery.easing.min.js",
                "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/Content/js/sb-admin-2.min.js",
                "~/Content/vendor/bootbox.all.min.js",
                "~/Content/vendor/bootbox.js",
                "~/Content/vendor/chart.js/Chart.min.js",
                "~/Content/js/demo/chart-area-demo.js",
                "~/Content/js/demo/chart-pie-demo.js"
                ));



        }

        
    }
}