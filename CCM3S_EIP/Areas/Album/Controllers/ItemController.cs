using CCM3S_EIP.App_Start;
using CCM3S_EIP.Areas.Album.Models;
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
    public class ItemController : Controller
    {
        EIP01Entities db = new EIP01Entities();
        AlbumsDBService albumDBservice = new AlbumsDBService();

        // GET: Albums/Item
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult ItemMaster(int? id)
        {

            PU_ALBUMS itemMaster = db.PU_ALBUMS.Find(id);
            ViewBag.pid = id;
            return View(itemMaster);
        }

        #region 查詢零件照片資料
        [UserTraceLog]
        public ActionResult ItemAlbum(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.SearchString = searchString;

            var pu_albums = albumDBservice.GetPUAlbumsData(searchString);

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var result = pu_albums.ToPagedList(pageNumber, pageSize);

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

        #region 查詢零件明細照片資料
        [UserTraceLog]
        public ActionResult ItemDetailAlbum(int id,string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.SearchString = searchString;

            var pu_albums = albumDBservice.GetPUAlbumsDetailData(searchString,id);

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var result = pu_albums.ToPagedList(pageNumber, pageSize);

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

        #region 下載檔案
        public ActionResult GetFilePath(int? id)
        {
            PU_ALBUMS pa = db.PU_ALBUMS.Find(id);

            if (!string.IsNullOrEmpty(pa.ImgPath) && System.IO.File.Exists(Server.MapPath(pa.ImgPath)))
                return File(System.IO.File.ReadAllBytes(Server.MapPath(pa.ImgPath)),
                 "application/unknown",
                 HttpUtility.UrlEncode(System.IO.Path.GetFileName(pa.ImgPath)));
            else
                return View();
        }
        #endregion

    }
}