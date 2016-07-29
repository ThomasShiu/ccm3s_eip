using System.Web;
using System.Web.Optimization;

namespace CCM3S_EIP
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery223").Include(
                        "~/Scripts/jquery-2.2.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/highchart").Include(
                        "~/Scripts/highcharts.js"));

            bundles.Add(new ScriptBundle("~/FullCalendar/jquery").Include(   
                        "~/Scripts/fullcalendar-2.8.0/lib/jquery.min.js",
                        "~/Scripts/fullcalendar-2.8.0/lib/moment.min.js",
                        "~/Scripts/fullcalendar-2.8.0/fullcalendar.min.js",
                        "~/Scripts/fullcalendar-2.8.0/lang-all.js",
                        "~/Scripts/jquery.ba-dotimeout.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/dotimeout").Include(
                       "~/Scripts/jquery-2.2.3.min.js",
                       "~/Scripts/jquery.ba-dotimeout.min.js"));
            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //行事曆使用style
            bundles.Add(new StyleBundle("~/FullCalendar/css").Include(
                      "~/Scripts/fullcalendar-2.8.0/lib/cupertino/jquery-ui.min.css",
                      "~/Scripts/fullcalendar-2.8.0/fullcalendar.css"));
        }
    }
}
