using CCM3S_EIP.App_Start;
using CCM3S_EIP.Areas.Album.Models;
using CCM3S_EIP.Areas.Album.Service;
using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace CCM3S_EIP.Areas.Album.Controllers
{
    public class ManageAlbumController : Controller
    {
        EIP01Entities db = new EIP01Entities();
        AlbumsDBService albumDBservice = new AlbumsDBService();
        UploadItemViewModel vm = new UploadItemViewModel();


        [UserTraceLog]
        public ActionResult AlbumIndex(string sortOrder, string currentFilter, string searchString, int? page)
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
        }

        #region 查詢零件明細照片資料
        [UserTraceLog]
        public ActionResult AlbumDetail(int id, string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.SearchString = searchString;
            ViewBag.parentId = id;

            var pu_albums = albumDBservice.GetPUAlbumsDetailData(searchString, id);

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

        [HttpGet]
        [UserTraceLog]
        public ActionResult UploadImg()
        {
            AlbumsDBService albumservice = new AlbumsDBService();
            UploadItemViewModel vm = new UploadItemViewModel();
            return View(vm);
        }

        /// <summary>
        /// 表單提交
        /// </summary>
        /// <param name="act"></param>
        /// <param name="vm"></param>
        /// <param name="myFile"></param>
        /// <returns></returns>
        
        [HttpPost]
        [UserTraceLog]
        public ActionResult UploadImg(string act, UploadItemViewModel vm, HttpPostedFileBase myFile)
        {
            switch (act)
            {
                case "upload"://上傳照片
                    this.UploadPhoto(vm, myFile);
                    break;
                case "post"://存檔，寫DB
                    string strFileName="";
                    foreach (string strPhotoFileName in vm.PhotoFileNames)
                    {
                        strFileName = strPhotoFileName;
                    }
                    
                    //存放檔案路徑
                    string strFilePath = "~/EIPContent/Content/PublicShare/VibrationPlate/" + strFileName;
                    vm.pu_album.ImgPath = strFilePath; //圖檔路徑
                    vm.pu_album.parentId = 0;

                    albumDBservice.InsertItemData(vm.pu_album);
                    //照片已在剛剛就上傳到Server
                    //this.LogWrite(vm);
                    return RedirectToAction("AlbumIndex", "ManageAlbum");
                    //break;
            }

            return View(vm);
        }



        [ChildActionOnly]
        public ActionResult ItemMaster(int? id)
        {

            PU_ALBUMS itemMaster = db.PU_ALBUMS.Find(id);
            return View(itemMaster);
        }


        [UserTraceLog]
        [HttpGet]
        public ActionResult UploadImg2(int? id)
        {

            UploadItemViewModel vm = new UploadItemViewModel();

            //PU_ALBUMS pu_album = new PU_ALBUMS();
            // var pu_album = db.PU_ALBUMS.Where(s => s.Id.Equals(id));
   
            return View(vm);
        }

        [UserTraceLog]
        [HttpPost]
        public ActionResult UploadImg2(string act, UploadItemViewModel vm, HttpPostedFileBase myFile,int? id)
        {
            switch (act)
            {
                case "upload"://上傳照片
                    this.UploadPhoto(vm, myFile);
                    break;
                case "post"://存檔，寫DB
                    try
                    {
                        string strFileName = "";
                        foreach (string strPhotoFileName in vm.PhotoFileNames)
                        {
                            strFileName = strPhotoFileName;
                        }

                        //存放檔案路徑
                        string strFilePath = "~/EIPContent/Content/PublicShare/VibrationPlate/" + strFileName;
                        vm.pu_album.ImgPath = strFilePath; //圖檔路徑
                        vm.pu_album.parentId = id;

                        albumDBservice.InsertItemData(vm.pu_album);
        
                        //照片已在剛剛就上傳到Server
                        //this.LogWrite(vm);
                        return RedirectToAction("ItemDetailAlbum", "UploadItem", new { id = id});
                    }
                    catch (Exception ex)
                    {
                        string v_error = ex.ToString();
                    }
                    break;
            }



            return View(vm);
        }
        [UserTraceLog]
        [HttpGet]
        public ActionResult UploadImgMulti(int? id)
        {

            UploadItemViewModel vm = new UploadItemViewModel();
            ViewBag.pid = id;
            //PU_ALBUMS pu_album = new PU_ALBUMS();
            // var pu_album = db.PU_ALBUMS.Where(s => s.Id.Equals(id));

            return View(vm);
        }

        #region 批次上傳圖片
        [UserTraceLog]
        [HttpPost]
        public ActionResult BatchUpload(int id)
        {
            UploadItemViewModel vm = new UploadItemViewModel();
            PU_ALBUMS pa = db.PU_ALBUMS.Find(id);
            

            bool isSavedSuccessfully = true;
            int count = 0;
            string msg = "";

            string fileName = "";
            string fileExtension = "";
            string filePath = "";
            string fileNewName = "";

            //这里是获取隐藏域中的数据
            //int albumId = string.IsNullOrEmpty(Request.Params["hidAlbumId"]) ?
            //    0 : int.Parse(Request.Params["hidAlbumId"]);

            try
            {
                string directoryPath = Server.MapPath("~/EIPContent/Content/PublicShare/VibrationPlate/");
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                foreach (string f in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[f];

                    if (file != null && file.ContentLength > 0)
                    {
                        fileName = file.FileName;
                        fileExtension = Path.GetExtension(fileName);
                        fileNewName = Guid.NewGuid().ToString() + fileExtension;
                        filePath = Path.Combine(directoryPath, fileNewName);
                        file.SaveAs(filePath);

                        count++;

                        //存放檔案路徑
                        pa.ImgPath = "~/EIPContent/Content/PublicShare/VibrationPlate/" + fileNewName; //圖檔路徑
                        pa.parentId = id;
                        pa.FileName = pa.FileName;
                        pa.Descript = pa.Descript;
                        pa.KindType1 = pa.KindType1;

                        albumDBservice.InsertItemData(pa);

                        //照片已在剛剛就上傳到Server
                        //this.LogWrite(vm);
         
                    }
                }
                msg = "succuss";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                isSavedSuccessfully = false;
            }

            return Json(new
            {
                Result = isSavedSuccessfully,
                Count = count,
                Message = msg
            });
        }
        #endregion

        #region 拖拉批次上傳圖片
        [HttpPost]
        public ActionResult UploadImgMulti(string act, UploadItemViewModel vm, HttpPostedFileBase myFile, int? id)
        {
            switch (act)
            {
                case "upload"://上傳照片
                    this.UploadPhoto(vm, myFile);
                    break;
                case "post"://存檔，寫DB
                    try
                    {
                        string strFileName = "";
                        foreach (string strPhotoFileName in vm.PhotoFileNames)
                        {
                            strFileName = strPhotoFileName;
                        }

                        //存放檔案路徑
                        string strFilePath = "~/EIPContent/Content/PublicShare/VibrationPlate/" + strFileName;
                        vm.pu_album.ImgPath = strFilePath; //圖檔路徑
                        vm.pu_album.parentId = id;

                        albumDBservice.InsertItemData(vm.pu_album);

                        //照片已在剛剛就上傳到Server
                        //this.LogWrite(vm);
                        return RedirectToAction("AlbumDetail", "UploadItem", new { id = id });
                    }
                    catch (Exception ex)
                    {
                        string v_error = ex.ToString();
                    }
                    break;
            }



            return View(vm);
        }
        #endregion

        #region 記錄LOG
        /// <summary>
        /// 寫Log查看表單post的結果
        /// </summary>
        /// <param name="vm"></param>
        private void LogWrite(UploadItemViewModel vm)
        {
            //寫Log - 檔案名稱(知道上傳的檔案名稱，就可以寫DB了)
            using (FileStream fs = new FileStream(@"D:\myLog.txt", FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs);

                foreach (string strPhotoFileName in vm.PhotoFileNames)
                {
                    sw.WriteLine(strPhotoFileName);
                }
                sw.Close();
            }

        }
        #endregion

        #region 單張上傳照片
        /// <summary>
        /// 上傳單一照片
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="myFile"></param>
        private void UploadPhoto(UploadItemViewModel vm, HttpPostedFileBase myFile)
        {
            if (myFile != null && myFile.ContentLength > 0)//使用者有選擇照片檔案
            {
                //新的檔案名稱
                string strFileName = Guid.NewGuid().ToString() + Path.GetExtension(myFile.FileName);
                //存放檔案路徑
                string strFilePath = Server.MapPath("~/EIPContent/Content/PublicShare/VibrationPlate/" + strFileName);
                //檔案存放在Server
                myFile.SaveAs(strFilePath);
                //ViewModel的PhotoFileNames累加一張圖片名稱
                vm.PhotoFileNames.Add(strFileName);
            }
        }
        #endregion

        #region  刪除照片集
        [UserTraceLog]  
        public ActionResult DeleteImg(int? id,string act, int pid, int? page)
        {
            int pageNumber = (page ?? 1);
            UploadItemViewModel vm = new UploadItemViewModel();

            //刪除主要照片集
            PU_ALBUMS pu_album = db.PU_ALBUMS.Find(id);

            
            vm.pu_album = pu_album;
            //刪除主機端照片
            string fullPath = Server.MapPath(vm.pu_album.ImgPath.ToString());
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            
            //刪除資料庫資料列
            db.PU_ALBUMS.Remove(pu_album);
            db.SaveChanges();

            try
            {
                //刪除子照片集
                var pa_detail = db.PU_ALBUMS.Where(x => x.parentId == pu_album.Id).ToList();
                if (pa_detail.Count > 0)
                {
    
                    db.PU_ALBUMS.RemoveRange(db.PU_ALBUMS.Where(c => c.parentId == pu_album.Id));
                    db.SaveChanges();

                    foreach (var s in pa_detail)
                    {
                        //刪除主機端照片
                        fullPath = Server.MapPath(s.ImgPath.ToString());
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                    
                }
            }
            catch(Exception ex) {
                return View("Error", new HandleErrorInfo(ex, "ManageAlbum", "AlbumIndex"));
                //ModelState.AddModelError("", ex.Message);
                //return View();
            }

            switch (act)
            {
                case "master":
                    return RedirectToAction("AlbumIndex", "ManageAlbum");
                case "detail":
                    return RedirectToAction("AlbumDetail", "ManageAlbum", new { @id = pid, @page = pageNumber });
                default:
                    return RedirectToAction("AlbumIndex", "ManageAlbum");
            }
    }
        #endregion

        #region  刪除照片集
        [UserTraceLog]
        public ActionResult DeleteAlbum(int? id, string act, int? page)
        {
            int pageNumber = (page ?? 1);
            UploadItemViewModel vm = new UploadItemViewModel();

            //刪除主要照片集
            PU_ALBUMS pu_album = db.PU_ALBUMS.Find(id);


            vm.pu_album = pu_album;
            //刪除主機端照片
            string fullPath = Server.MapPath(vm.pu_album.ImgPath.ToString());
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            //刪除資料庫資料列
            db.PU_ALBUMS.Remove(pu_album);
            db.SaveChanges();

            try
            {
                //刪除子照片集
                var pa_detail = db.PU_ALBUMS.Where(x => x.parentId == pu_album.Id).ToList();
                if (pa_detail.Count > 0)
                {

                    db.PU_ALBUMS.RemoveRange(db.PU_ALBUMS.Where(c => c.parentId == pu_album.Id));
                    db.SaveChanges();

                    foreach (var s in pa_detail)
                    {
                        //刪除主機端照片
                        fullPath = Server.MapPath(s.ImgPath.ToString());
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "ManageAlbum", "AlbumIndex"));
                //ModelState.AddModelError("", ex.Message);
                //return View();
            }

            switch (act)
            {
                case "master":
                    return RedirectToAction("AlbumIndex", "ManageAlbum", new { @page = pageNumber });
                default:
                    return RedirectToAction("AlbumIndex", "ManageAlbum");
            }

        }
        #endregion


    }

}