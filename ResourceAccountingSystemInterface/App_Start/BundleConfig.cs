using System.Web;
using System.Web.Optimization;

namespace ResourceAccountingSystemInterface
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                        "~/Scripts/vendor.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                        "~/Scripts/JavaScript.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/css/styles.min.css"));
        }
    }
}
