using System.Web;
using System.Web.Optimization;

namespace RMS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            //"~/Content/Scripts/jquery-{version}.js",
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         //"~/Content/Scripts/jquery.js",
                         "~/Content/Scripts/jquery.simple.timer.js",
                         "~/Content/Scripts/jquery-1.7.1.js",
                         "~/Content/Scripts/jquery-1.7.1.intellisense.js",
                         "~/Content/Scripts/rms.js"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Content/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.validate*",
                /*Neelam Issue Id:60562 start*/
                        "~/Content/Scripts/jquery.validate.unobtrusive*",
                /*Neelam Issue Id:60562 end*/
                        "~/Content/Scripts/jquery.unobtrusive*"));                        


            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/css/menu.css",
               "~/Content/css/StyleSheet.css",               
               "~/Content/css/bootstrapcss/*.css",
               "~/Content/css/bootstrapcss/*.css.map",
               "~/Content/css/bootstrapfont/glyphicons-halflings-regular.*",
               "~/Content/css/bootstrapcss/datepicker.css",
               "~/Content/css/MRFCommon.css",
               "~/Content/css/rms_custom.css"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}