using System.Web;
using System.Web.Optimization;

namespace devarts
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear(); 
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/themes/base/core.css",
                      "~/Content/themes/base/datepicker.css",
                      "~/Content/themes/base/theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxuntro").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/devarts").Include(
                        "~/Scripts/devArts.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/summernote").Include(
                        "~/Scripts/summernote/summernote.js",
                        "~/Scripts/summernote/plugin/databasic/summernote-ext-databasic.js",
                        "~/Scripts/summernote/plugin/hello/summernote-ext-hello.js",
                        "~/Scripts/summernote/plugin/specialchars/summernote-ext-specialchars.js",
                        "~/Scripts/summernote/lang/summernote-pl-PL.js",
                        "~/Scripts/summernote/defaultSummernote.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/summernote").Include(
                        "~/Scripts/summernote/summernote.css",
                        "~/Scripts/summernote/plugin/databasic/summernote-ext-databasic.css",
                        "~/Scripts/summernote/font/summernote.eot",
                        "~/Scripts/summernote/font/summernote.ttf",
                        "~/Scripts/summernote/font/summernote.woff"));

            bundles.Add(new StyleBundle("~/Content/fontAwesome.css").Include("~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileUpload").Include(
                       "~/Scripts/jquery.fileupload.js",
                       "~/Scripts/jquery.iframe-transport.js"));

            // Galeria
            bundles.Add(new StyleBundle("~/MAGallery/css").Include("~/MAGallery/css/MAgallery.css"));
            bundles.Add(new ScriptBundle("~/MAGallery/js").Include(
                       "~/MAGallery/js/MAgallery.js"));

            // DataTables
            // 1. Ładowanie styli
            // 2. Ładowanie bibliotek JS

            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                        "~/Content/dataTablesStyles.css",
                        "~/Content/DataTables/css/dataTables.bootstrap.min.css",
                        "~/Content/DataTables/css/responsive.bootstrap.min.css"));

            /// skrypty dla Administratorów
            bundles.Add(new ScriptBundle("~/Scripts/AdminDataTables").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap4.min.js",
                        "~/Scripts/DataTablesInViews/adminDataTablesScripts.js"));

            /// skrypty dla modułu obsługi strony dla hodowców - webu
            bundles.Add(new ScriptBundle("~/Scripts/PostDataTables").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap4.min.js",
                        "~/Scripts/DataTablesInViews/webBreedDataTablesScripts.js"));

            /// skrypty dla modułu obsługi hodowli
            bundles.Add(new ScriptBundle("~/Scripts/KennelDataTables").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap4.min.js",
                        "~/Scripts/DataTablesInViews/kennelDataTablesScripts.js"));

            /// skrypty dla modułu obsługi hodowli
            bundles.Add(new ScriptBundle("~/Scripts/AssistantDataTables").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap4.min.js",
                        "~/Scripts/DataTablesInViews/assistantDataTablesScripts.js"));

            /// skrypty dla absolwentów
            bundles.Add(new ScriptBundle("~/Scripts/GraduateDataTables").Include(
              "~/Scripts/jquery.dataTables.min.js",
              "~/Scripts/dataTables.bootstrap4.min.js",
              "~/Scripts/DataTablesInViews/graduateDataTablesScripts.js"));

            /// CUSTOM STYLE
            bundles.Add(new StyleBundle("~/Web/css/custom_style.css").Include("~/Web/css/custom_style.css"));

            /// PEDIGREE
            bundles.Add(new StyleBundle("~/Content/Features/Pedigree/pedigree.css").Include("~/Content/Features/Pedigree/pedigree.css"));

            // PUREBREED-owe style
            bundles.Add(new StyleBundle("~/Content/purebreed.css").Include("~/Content/purebreed.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
