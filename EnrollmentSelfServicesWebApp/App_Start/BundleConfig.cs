using System.Web;
using System.Web.Optimization;

namespace EnrollmentSelfServicesWebApp
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/wwwroot/lib/jquery/dist/jquery.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/wwwroot/lib/jquery-validation/dist/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/wwwroot/lib/bootstrap/dist/js/bootstrap.js"));

            bundles.Add(new StyleBundle("~/wwwroot/css").Include(
                      "~/wwwroot/lib/bootstrap/dist/css/bootstrap.css",
                      "~/wwwroot/css/site.css"));
        }
    }
}
