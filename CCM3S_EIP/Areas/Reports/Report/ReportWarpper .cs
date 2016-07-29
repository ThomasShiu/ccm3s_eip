using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM3S_EIP.Areas.Reports.Report
{

    public class ReportWrapper
    {
        // Constructors
        public ReportWrapper()
        {
            ReportDataSources = new List<ReportDataSource>();
            ReportParameters = new List<ReportParameter>();
        }


        // Properties
        public string ReportPath { get; set; }

        public List<ReportDataSource> ReportDataSources { get; set; }

        public List<ReportParameter> ReportParameters { get; set; }

        public bool IsDownloadDirectly { get; set; }
    }

}
