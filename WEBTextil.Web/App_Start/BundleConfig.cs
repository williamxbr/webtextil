using System.Web;
using System.Web.Optimization;

namespace WEBTextil.Web
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string lib = "~/lib/";
            string js = "~/js/";

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(lib + "jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/lib")
                .Include(lib + "jquery-ui/jquery-ui.min.js")
                .Include(lib + "jquery-validade/jquery-validate.min.js")
                .Include(lib + "bootstrap/js/bootstrap.bundle.js")
                .Include(lib + "bootstrap-colorpicker/js/bootstrap-colorpicker.js")
                .Include(lib + "moment/moment.min.js")
                .Include(lib + "moment/moment-with-locales.js")
                .Include(lib + "daterangepicker/daterangepicker.js")
                .Include(lib + "dropzone/dropzone.js")
                .Include(lib + "bootstrap-switch/js/bootstrap-switch.js")
                .Include(lib + "fullcalendar/main.min.js")
                .Include(lib + "fullcalendar/locales/pt-br.js")
                .Include(lib + "select2/js/select2.full.min.js")
                .Include(lib + "toastr/toastr.min.js")
                .Include(lib + "bootbox/bootbox.js")
                .Include(lib + "jsgrid/jsgrid.min.js")
                .Include(lib + "jsgrid/i18n/jsgrid-pt-br.js")
                .Include(lib + "accounting/accounting.js")
                .Include(lib + "jquery-easing/jquery.easing.min.js")
                .Include(lib + "jquery-mask/jquery.mask.min.js")
                .Include(lib + "sweetalert2/sweetalert2.all.js")
                .Include(lib + "sparklines/sparkline.js")
                .Include(lib + "summernote/summernote-bs4.js")
                .Include(lib + "jqvmap/jquery.vmap.js")
                .Include(lib + "jqvmap/maps/jquery.vmap.brazil.js")
                .Include(lib + "jquery-knob/jquery.knob.min.js")
                .Include(lib + "chart.js/Chart.js")
                .Include(lib + "tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.js")
                .Include(lib + "overlayScrollbars/js/jquery.overlayScrollbars.js")
                .Include(lib + "adminlte/js/adminlte.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(lib + "tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.css")
                .Include(lib + "jqueryui/jquery-ui.min.css")
                .Include(lib + "bootstrap-colorpicker/css/bootstrap-colorpicker.css")
                .Include(lib + "bootstrap-daterangepicker/daterangepicker.css")
                //.Include(lib + "bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css")
                .Include(lib + "dropzone/dropzone.css")
                .Include(lib + "bootstrap-slider/slider.css")
                .Include(lib + "select2/css/select2.min.css")
                .Include(lib + "select2-bootstrap4-theme/select2-bootstrap4.min.css")
                .Include(lib + "bootstrap-colorpicker/css/bootstrap-colorpicker.min.css")
                .Include(lib + "timepicker/bootstrap-timepicker.min.css")
                .Include(lib + "icheck-bootstrap/icheck-bootstrap.css")
                .Include(lib + "overlayScrollbars/css/OverlayScrollbars.css")
                .Include(lib + "pace/pace.min.css")
                .Include(lib + "toastr/toastr.min.css")
                .Include(lib + "fullcalendar/main.css")
                .Include(lib + "summernote/summernote-bs4.css")
                .Include(lib + "jqvmap/jqvmap.css")
                .Include(lib + "chart.js/Chart.css")
                .Include(lib + "jsgrid/jsgrid.min.css")
                .Include(lib + "jsgrid/jsgrid-theme.min.css")
                .Include(lib + "adminlte/css/adminlte.min.css"));

            bundles.Add(new StyleBundle("~/bundles/webtextilcss").Include(
                       "~/css/webtextil.css"));

            bundles.Add(new ScriptBundle("~/bundles/webtextil")
                .IncludeDirectory(js,"*.js"));
        }
    }
}
