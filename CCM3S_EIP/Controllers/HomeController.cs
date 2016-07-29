using CCM3S_EIP.App_Start;
using CCM3S_EIP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CCM3S_EIP.Controllers
{
    public class HomeController : Controller
    {
        private EIP01Entities db = new EIP01Entities();

        [UserTraceLog]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly] //只能有子程序帶入
        public ActionResult BookingCarView()
        {
            //ViewData.Model = db.Table.ToList();
            var query = db.VIEW_PO_PUBLIC_OBJECT_BOOKING_CAR.OrderByDescending(x => x.EMP_NM);
            ViewData.Model = query.ToList();
            ViewData["count"] = query.Count();
            return View();

        }

        [ChildActionOnly] //只能有子程序帶入
        public ActionResult BookingMeetingRoomView()
        {
            //ViewData.Model = db.Table.ToList();
            var query = db.VIEW_PO_PUBLIC_OBJECT_BOOKING_MEETINGROOM.OrderByDescending(x => x.EMP_NM);
            ViewData.Model = query.ToList();
            ViewData["count"] = query.Count();
            return View();

        }

        //公務車行事曆
        [UserTraceLog]
        public ActionResult CarCalendar()
        {
            return View();
        }

        //會議室行事曆
        [UserTraceLog]
        public ActionResult MeetRoomCalendar()
        {
            //ViewBag.carJson = " {title: 'All Day Event',start: '2016-07-01'}";
            return View();
        }

        public ActionResult testView()
        {
            return View();
        }

        [ChildActionOnly] //只能有子程序帶入
        public ActionResult List_ChildView()
        {
            //ViewData.Model = db.Table.ToList();
            var query = db.VIEW_PO_PUBLIC_OBJECT_BOOKING_CAR.OrderByDescending(x => x.ObjectSID);
            ViewData.Model = query.ToList();
            ViewData["count"] = query.Count();
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult carCalendarBooking(string start, string end)
        {
            //convert string to date
            DateTime _start = DateTime.TryParse(start, out _start) ? _start : DateTime.Now.Date;
            DateTime _end = DateTime.TryParse(end, out _end) ? _end : DateTime.Now.Date;

            var result = from C in db.V_BOOKING_CAR
                         where (C.BookingStartTime != null || C.BookingEndTIme != null)
                         select new { id = C.BOOKING_SID, title = C.ObjectNM + "-" + C.EMP_NM, start = (C.BookingStartTime), end = (C.BookingEndTIme), allDay = false };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult meetroomCalendarBooking(string start, string end)
        {
            //convert string to date
            DateTime _start = DateTime.TryParse(start, out _start) ? _start : DateTime.Now.Date;
            DateTime _end = DateTime.TryParse(end, out _end) ? _end : DateTime.Now.Date;

            var result = from C in db.V_BOOKING_MEETROOM
                         where (C.BookingStartTime != null || C.BookingEndTIme != null)
                         select new { id = C.BOOKING_SID, title = C.ObjectNM + "-" + C.EMP_NM, start = (C.BookingStartTime), end = (C.BookingEndTIme), allDay = false };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //利用Ajax 向Server端要資料
        //因為MVC有限制 GET 時不能丟出Json(需加JsonRequestBehavior.AllowGet)
        //所以方便起見就用 POST 吧。
        //[HttpPost]
        //public ActionResult GetCarBookingData()
        //{
        //    ReportData data = new ReportData();
        //    DateTime now_Subtraction = DateTime.Now.AddHours(-48);

        //    var result = from C in db.V_BOOKING_CAR
        //                 where (C.BookingStartTime != null || C.BookingEndTIme != null)
        //                 select new { id = C.BOOKING_SID, title = C.ObjectNM + "-" + C.EMP_NM, start = (C.BookingStartTime), end = (C.BookingEndTIme), allDay = false };


        //    //用MVC內建的JsonResult丟回資料。
        //    return Json(result);

        //}

        //自訂一個類別，裡面有兩個List<int>的屬性，Category1跟Category2
        //等等出來的報表會有兩條線
        //public class ReportData
        //{
        //    public List<string> xBar_list { get; set; }
        //    public List<int?> a_cnt_list { get; set; }
        //    public List<int?> o_cnt_list { get; set; }
        //}

        //public class CharData
        //{
        //    public DateTime cdate;
        //    public int? a_cnt;
        //    public int? o_cnt;
        //}

    }
}