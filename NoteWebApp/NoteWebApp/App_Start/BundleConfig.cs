using System.Web;
using System.Web.Optimization;

namespace NoteWebApp
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                        "~/Scripts/jquery.validate*"));
          
            bundles.Add(new ScriptBundle("~/Scripts").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/Scripts").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/App_LocalResourses/css")
                .Include("~/Content/App_LocalResourses/localStyleSheet.css"));
        }
    }
}
