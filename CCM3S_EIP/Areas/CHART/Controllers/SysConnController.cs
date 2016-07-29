using CCM3S_EIP.App_Start;
using CCM3S_EIP.Areas.Chart.Models;
using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCM3S_EIP.Areas.Chart.Controllers
{
    /*
     *  各系統連線狀態統計圖表 2016-7-27
     * 
     */
    public class SysConnController : Controller
    {
        EIP01Entities db = new EIP01Entities();
        ChartModel chartmodel = new ChartModel();

        // GET: CHART/PdmConnChart
        [UserTraceLog]
        public ActionResult Index()
        {
            return View();
        }

       
        //MS Chart使用
        public class newItem
        {
            public DateTime dt { get; set; }
            public int? a_cnt { get; set; }
            public int? o_cnt { get; set; }

            public string FormattedDate
            {
                get { return dt.ToString("MM-dd H"); }
            }
        }


        public void ChartIndex()
        {
            //   // MyDataContext db = new MyDataContext();

            //    //建立一個新的Chart
            //    Chart chart = new Chart();
            //    chart.ID = "Chart1";
            //    chart.Height = System.Web.UI.WebControls.Unit.Pixel(600);
            //    chart.Width = System.Web.UI.WebControls.Unit.Pixel(1200);
            //    //建立一個ChartAreas，並對ChartAreas設定格式
            //    chart.ChartAreas.Add("ChartAreas1");
            //    chart.ChartAreas["ChartAreas1"].BorderColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //    chart.ChartAreas["ChartAreas1"].BorderDashStyle = ChartDashStyle.Solid;
            //    chart.ChartAreas["ChartAreas1"].BackSecondaryColor = System.Drawing.Color.White;
            //    chart.ChartAreas["ChartAreas1"].BackColor = System.Drawing.Color.FromArgb(64, 165, 191, 228);
            //    chart.ChartAreas["ChartAreas1"].ShadowColor = System.Drawing.Color.Transparent;
            //    chart.ChartAreas["ChartAreas1"].BackGradientStyle = GradientStyle.TopBottom;
            //    chart.ChartAreas["ChartAreas1"].Area3DStyle.Rotation = 10;
            //    chart.ChartAreas["ChartAreas1"].Area3DStyle.Perspective = 10;
            //    chart.ChartAreas["ChartAreas1"].Area3DStyle.Inclination = 15;
            //    chart.ChartAreas["ChartAreas1"].Area3DStyle.IsRightAngleAxes = false;
            //    chart.ChartAreas["ChartAreas1"].Area3DStyle.WallWidth = 0;
            //    chart.ChartAreas["ChartAreas1"].Area3DStyle.IsClustered = false;
            //    chart.ChartAreas["ChartAreas1"].AxisY.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //    chart.ChartAreas["ChartAreas1"].AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25f, System.Drawing.FontStyle.Bold);
            //    chart.ChartAreas["ChartAreas1"].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //    chart.ChartAreas["ChartAreas1"].AxisX.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //    chart.ChartAreas["ChartAreas1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25f, System.Drawing.FontStyle.Bold);
            //    chart.ChartAreas["ChartAreas1"].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);

            //    //新增一個Series
            //    chart.Series.Add("Series1");

            //    //select yymmdd,sum(online_cnt) from PDM_CONNECT_COUNT group by to_char(create_date,'yyyyMMdd HH24mi')
            //    var result = (from el in db.PDM_CONNECT_COUNT
            //                  orderby el.CREATE_DATE 
            //                 select el into g
            //                 select new newItem
            //                 {
            //                     dt = g.CREATE_DATE,
            //                     a_cnt = g.ACCOUNT_CNT,
            //                     o_cnt = g.ON_LINE_CNT
            //                 }).Take(48);

            //    //將資料塞入Chart Controls
            //    foreach (var item in result)
            //    {
            //        chart.Series["Series1"].Points.AddXY(item.FormattedDate, item.a_cnt);
            //    }

            //    //顯示長條圖的數據
            //    chart.Series["Series1"].IsValueShownAsLabel = true;

            //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //    chart.SaveImage(ms, ChartImageFormat.Png);
            //    ms.Seek(0, System.IO.SeekOrigin.Begin);

            //    return File(ms, "image/png");
            //}

            //public ActionResult GetChart()
            //{
            //    Chart c = new Chart();
            //    try
            //    {
            //        c.ID = "Chart1";
            //        c.Height = System.Web.UI.WebControls.Unit.Pixel(600);
            //        c.Width = System.Web.UI.WebControls.Unit.Pixel(1200);
            //        c.Palette = ChartColorPalette.BrightPastel;
            //        c.ImageType = ChartImageType.Png;
            //        c.BorderlineDashStyle = ChartDashStyle.Solid;
            //        c.BackSecondaryColor = System.Drawing.Color.White;
            //        c.BackGradientStyle = GradientStyle.TopBottom;
            //        c.BorderlineWidth = 2;
            //        c.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3DFF0");

            //        c.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;

            //        ChartArea ca = new ChartArea();
            //        ca.Name = "ChartArea1";
            //        ca.BorderColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //        ca.BorderDashStyle = ChartDashStyle.Solid;
            //        ca.BackSecondaryColor = System.Drawing.Color.White;
            //        ca.BackColor = System.Drawing.Color.FromArgb(64, 165, 191, 228);
            //        ca.ShadowColor = System.Drawing.Color.Transparent;
            //        ca.BackGradientStyle = GradientStyle.TopBottom;
            //        ca.Area3DStyle.Rotation = 10;
            //        ca.Area3DStyle.Perspective = 10;
            //        ca.Area3DStyle.Inclination = 15;
            //        ca.Area3DStyle.IsRightAngleAxes = false;
            //        ca.Area3DStyle.WallWidth = 0;
            //        ca.Area3DStyle.IsClustered = false;
            //        ca.AxisY.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //        ca.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25f, System.Drawing.FontStyle.Bold);
            //        ca.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //        ca.AxisX.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //        ca.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25f, System.Drawing.FontStyle.Bold);
            //        ca.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            //        c.ChartAreas.Add(ca);

            //        Legend cl = new Legend();
            //        cl.Name = "Default1";
            //        cl.IsTextAutoFit = true;
            //        cl.BackColor = System.Drawing.Color.Transparent;
            //        cl.Font = new System.Drawing.Font("Trebuchet MS", 8.25f, System.Drawing.FontStyle.Bold);
            //        cl.IsDockedInsideChartArea = false;
            //        cl.DockedToChartArea = "ChartArea1";
            //        c.Legends.Add(cl);

            //        //讓顯示的X，不會跳著顯示
            //        c.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            //        DateTime now_Subtraction = DateTime.Now.AddHours(-32);

            //        var result = from el in db.PDM_CONNECT_COUNT
            //                     where el.CREATE_DATE >= now_Subtraction
            //                     orderby el.CREATE_DATE
            //                     select el into g
            //                     select new newItem
            //                     {
            //                         dt = g.CREATE_DATE,
            //                         a_cnt = g.ACCOUNT_CNT,
            //                         o_cnt = g.ON_LINE_CNT
            //                     };

            //        Series s = new Series();
            //        s.Name = "線上人數";
            //        c.Series.Add("線上人數");
            //        c.Series["線上人數"].Points.DataBind(result, "FormattedDate", "o_cnt", null);
            //        c.Series["線上人數"].ChartArea = "ChartArea1";
            //        c.Series["線上人數"].Legend = "Default1";
            //        //定義當滑鼠移到圖上時將呈現的數據或是文字
            //        c.Series["線上人數"].ToolTip = "Total: #VALY";

            //        //將說明在下方顯示並至中
            //        c.Legends["Default1"].Docking = Docking.Bottom;
            //        c.Legends["Default1"].Alignment = System.Drawing.StringAlignment.Center;

            //        c.Series["線上人數"]["PointWidth"] = "0.6";
            //        c.Series["線上人數"].Color = System.Drawing.Color.FromArgb(128, 65, 140, 240);
            //        //return c;

            //        //顯示長條圖的數據
            //        c.Series["線上人數"].IsValueShownAsLabel = true;
            //        c.Series["線上人數"].LabelAngle = 45;
            //        Series s2 = new Series();
            //        s2.Name = "連結數";
            //        c.Series.Add("連結數");
            //        c.Series["連結數"].Points.DataBind(result, "FormattedDate", "a_cnt", null);
            //        c.Series["連結數"].ChartArea = "ChartArea1";
            //        c.Series["連結數"].Legend = "Default1";
            //        //定義當滑鼠移到圖上時將呈現的數據或是文字
            //        c.Series["連結數"].ToolTip = "Total: #VALY";

            //        //將說明在下方顯示並至中
            //        c.Legends["Default1"].Docking = Docking.Bottom;
            //        c.Legends["Default1"].Alignment = System.Drawing.StringAlignment.Center;

            //        c.Series["連結數"]["PointWidth"] = "0.6";
            //        c.Series["連結數"].Color = System.Drawing.Color.FromArgb(128, 60, 0, 255);
            //        //return c;

            //        //顯示長條圖的數據
            //        c.Series["連結數"].IsValueShownAsLabel = true;

            //        System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //        c.SaveImage(ms, ChartImageFormat.Png);
            //        ms.Seek(0, System.IO.SeekOrigin.Begin);

            //        return File(ms, "image/png");
            //    }
            //    catch (Exception ex)
            //    {
            //        string v_ex = ex.ToString();

            //    }
            //    return null;
        }

        public ActionResult FourDayConn()
        {
            return View();
        }

   
        //利用Ajax 向Server端要資料
        //因為MVC有限制 GET 時不能丟出Json(需加JsonRequestBehavior.AllowGet)
        //所以方便起見就用 POST 吧。
        [HttpPost]
        public ActionResult GetConnData(string sysname)
        {
            ChartData data = new ChartData();

            DateTime before_Subtraction = DateTime.Now.AddHours(-60);
            DateTime now_Subtraction = DateTime.Now.AddHours(0);

            var query = (from m in db.PU_CONNECT_COUNT
                         where (m.CREATE_DATE >= before_Subtraction) & (m.SYS_NAME == sysname)
                         select new ColData { cdate = m.CREATE_DATE, a_cnt = m.ACCOUNT_CNT, o_cnt = m.ON_LINE_CNT }).ToList().
                         Select(x => new { cdate = x.cdate.ToString("M-dd HH:mm"), a_cnt = x.a_cnt, o_cnt = x.o_cnt });

            List<string> xbar = (from q in query select q.cdate).ToList();
            List<int?> a_cnt = (from q in query select q.a_cnt).ToList();
            List<int?> o_cnt = (from q in query select q.o_cnt).ToList();
            data.xBar_list = xbar.ToList();
            data.a_cnt_list = a_cnt.ToList();
            data.o_cnt_list = o_cnt.ToList();


            //用MVC內建的JsonResult丟回資料。
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetConnData2()
        {
            ChartData data = new ChartData();
            DateTime before_Subtraction = DateTime.Now.AddHours(-288);
            DateTime now_Subtraction = DateTime.Now.AddHours(0);

            var query = (from m in db.PU_CONNECT_COUNT
                         where (m.CREATE_DATE >= before_Subtraction | m.CREATE_DATE <= before_Subtraction) & (m.SYS_NAME == "PDM")
                         select new ColData { cdate = m.CREATE_DATE, a_cnt = m.ACCOUNT_CNT, o_cnt = m.ON_LINE_CNT }).ToList().
                         Select(x => new { cdate = x.cdate.ToString("M-dd HH:mm"), a_cnt = x.a_cnt, o_cnt = x.o_cnt });

            List<string> xbar = (from q in query select q.cdate).ToList();
            List<int?> a_cnt = (from q in query select q.a_cnt).ToList();
            List<int?> o_cnt = (from q in query select q.o_cnt).ToList();
            data.xBar_list = xbar.ToList();
            data.a_cnt_list = a_cnt.ToList();
            data.o_cnt_list = o_cnt.ToList();

            //用MVC內建的JsonResult丟回資料。
            return Json(data);
        }
    }


 
}