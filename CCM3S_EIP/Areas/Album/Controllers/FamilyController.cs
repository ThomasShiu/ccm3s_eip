using CCM3S_EIP.App_Start;
using CCM3S_EIP.Areas.Album.Service;
using CCM3S_EIP.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCM3S_EIP.Areas.Album.Controllers
{
    
    public class FamilyController : Controller
    {
        EIP01Entities db = new EIP01Entities();
        AlbumsDBService albumDBservice = new AlbumsDBService();

        // GET: Albums/Family
        public ActionResult Index()
        {
            return View();
        }
        #region 查詢員工照片資料
        [UserTraceLog]
        public ActionResult FamilyAlbum(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.SearchString = searchString;

            var hr_employee = albumDBservice.GetData(searchString);

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var result = hr_employee.ToPagedList(pageNumber, pageSize);

            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 1; i <= result.PageCount; i++)
            {
                items.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = i.Equals(result.PageNumber)
                });
            }
            ViewBag.PageItems = items;
            //頁數資訊
            ViewBag.PageCountAndCurrentLocation = string.Format(
                "第{0}頁 / 共{1}頁",
                result.PageNumber, result.PageCount);
            //資料列數資訊
            ViewBag.ItemSliceAndTotalFormat = string.Format(
                "顯示項目：{0} ~ {1},共{2}項",
                result.FirstItemOnPage,
                result.LastItemOnPage,
                result.TotalItemCount);

            return View(result);
            //return result;
        }
        #endregion

        #region 上傳照片頁面
        [UserTraceLog]
        public ActionResult UploadImg()
        {
            return View();
        }
        #endregion

        #region 上傳照片資料
        [UserTraceLog]
        [HttpPost]
        public ActionResult UploadImg(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/EIPContent/Content/PublicShare/VibrationPlate"), fileName);
                    file.SaveAs(path);

                    //寫入資料庫

                }
            }
            return RedirectToAction("UploadImg");
        }
        #endregion




    }
}