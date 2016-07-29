using CCM3S_EIP.App_Data;
using CCM3S_EIP.Areas.Reports.Report;
using CCM3S_EIP.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCM3S_EIP.Areas.MIS.Report
{


    public partial class RunReport1 : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GenerateReport();
            }
        }

        private void GenerateReport()
        {

            //LocalReport lr = new LocalReport();
            string _Path = Path.Combine(Server.MapPath("~/Areas/Reports/Report"), "Report1.rdlc");
            if (System.IO.File.Exists(_Path))
            {
                RptViewer.LocalReport.ReportPath = _Path;
                //lr.ReportPath = _Path;
            }
            // Set report data source
            RptViewer.LocalReport.DataSources.Clear();
            List<ActionLog> cm = new List<ActionLog>();
            using (EIP01Entities db = new EIP01Entities())
            {
                cm = db.ActionLog.ToList();
            }
            ReportDataSource rd = new ReportDataSource("ReportDataSet1", cm);
            RptViewer.LocalReport.DataSources.Add(rd);
         
            // Set report parameters
            //RptViewer.LocalReport.SetParameters(rw.ReportParameters);

            // Refresh report
            RptViewer.LocalReport.Refresh();

            // Download report directly
            //if (rw.IsDownloadDirectly)
            //{
            //    Warning[] warnings;
            //    string[] streamids;
            //    string mimeType;
            //    string encoding;
            //    string extension;

            //    byte[] bytes = RptViewer.LocalReport.Render(
            //       "Excel", null, out mimeType, out encoding, out extension,
            //       out streamids, out warnings);

            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment; filename=sample.xls");
            //    Response.AddHeader("Content-Length", bytes.Length.ToString());
            //    Response.ContentType = "application/octet-stream";
            //    Response.OutputStream.Write(bytes, 0, bytes.Length);
            //}

            // Remove session
            //Session[ReportWrapperSessionKey] = null;

        }
    }
}