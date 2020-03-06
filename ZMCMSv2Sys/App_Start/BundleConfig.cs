using System.Web;
using System.Web.Optimization;

namespace ZMCMSv2Sys
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/font-awesome.min.css",
                     "~/Content/ionicons.min.css",
                     "~/Content/Site.css",
                     "~/Content/Editor.css",
                     "~/admin-lte/css/AdminLTE.min.css",
                     "~/admin-lte/css/skins/_all-skins.min.css",
                     "~/Content/kendo/2018.2.516/kendo.common.min.css",
                     "~/Content/kendo/2018.2.516/kendo.mobile.all.min.css",
                     "~/Content/kendo/kendo.custom.css"
                     ));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                     "~/admin-lte/js/AdminLTE.min.js",
                     "~/admin-lte/js/pages/dashboard.js",
                     "~/admin-lte/js/demo.js",
                     "~/Scripts/kendo/2018.2.516/kendo.web.min.js",
                     "~/Scripts/kendo/2018.2.516/kendo.aspnetmvc.min.js",
                     "~/Scripts/kendo/2018.2.516/jszip.min.js",
                     "~/Scripts/kendo/2018.2.516/kendo.all.min.js",
                     "~/Scripts/kendo/2018.2.516/cultures/kendo.culture.zh-TW.min.js",
                     "~/Scripts/kendo/2018.2.516/messages/kendo.messages.zh-TW.min.js",
                     "~/Scripts/kendo/2018.2.516/pako_deflate.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/cshtmlcss").Include(
                     "~/Content/kendo/2018.2.516/kendo.common.min.css",
                     "~/Content/kendo/2018.2.516/kendo.mobile.all.min.css",
                     "~/Content/kendo/kendo.custom.css",
                     "~/Content/dtree.css"
                     ));

            bundles.Add(new ScriptBundle("~/Content/cshtmljs").Include(                     
                     "~/Scripts/kendo/2018.2.516/kendo.web.min.js",
                     "~/Scripts/kendo/2018.2.516/kendo.aspnetmvc.min.js",
                     "~/Scripts/kendo/2018.2.516/jszip.min.js",
                     "~/Scripts/kendo/2018.2.516/kendo.all.min.js",
                     "~/Scripts/kendo/2018.2.516/cultures/kendo.culture.zh-TW.min.js",
                     "~/Scripts/kendo/2018.2.516/messages/kendo.messages.zh-TW.min.js",
                     "~/Scripts/kendo/2018.2.516/pako_deflate.min.js",
                     "~/Scripts/dtree.js"
                     ));
        }
    }
}
