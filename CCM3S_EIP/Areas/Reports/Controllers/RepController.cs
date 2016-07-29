using CCM3S_EIP.App_Data;
using CCM3S_EIP.Areas.Reports.Report;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCM3S_EIP.Areas.Reports.Controllers
{
    public class RepController : Controller
    {
        // GET: Reports/Rep
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult BrowseReport()
        {
            //資料集
            EIP01DataSet ds = new EIP01DataSet();
            //ReportDataSet1 dd = new ReportDataSet1();

            //傳參數用
            ReportWrapper rw = new ReportWrapper();

            rw.ReportPath = Server.MapPath("~/Areas/Reports/Report/Report1.rdlc");
            rw.ReportDataSources.Add(new ReportDataSource("ReportDataSet1", ds.ActionLog.Copy()));
            rw.IsDownloadDirectly = false;
            Session["ReportWrapper"] = rw;
            return RedirectToAction("RunReport1.aspx", "Areas/Reports/Report", new { area = "" });
            //return Redirect(Url.Action("ViewReport", "ReportView"));

            //// Prepare data in report
            //EIP01DataSet ds = new EIP01DataSet();
            //ds.User.AddUserRow("chris", "chris chen", "Taipei");
            //ds.User.AddUserRow("eunice", "eunice chen", "New Taipei");

            //// Set report info
            //ReportWrapper rw = new ReportWrapper();
            //rw.ReportPath = Server.MapPath("/Report/Rdlc/UserRpt.rdlc");
            //rw.ReportDataSources.Add(new ReportDataSource("UserRptDataSet_User", ds.User.Copy()));
            //rw.ReportParameters.Add(new ReportParameter("RptMakerParam", "CHRIS"));
            //rw.IsDownloadDirectly = false;

            //// Pass report info via session
            //Session["ReportWrapper"] = rw;

            //// Go report viewer page
            //return Redirect("/Report/ReportViewer.aspx");
        }
    }
}