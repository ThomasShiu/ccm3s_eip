using CCM3S_EIP.Areas.Guestbook.Service;
using CCM3S_EIP.Areas.Guestbook.ViewModels;
using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCM3S_EIP.Areas.Guestbook.Controllers
{
  
    public class GBHomeController : Controller
    {
        GuestbooksDBService guestbookDBservice = new GuestbooksDBService();

        // GET: Guestbook/GBHome
        public ActionResult Index(String Search,int Page=1)
        {
            // 宣告一個新頁面模型
            GuestbookView Data = new GuestbookView();
            // 將傳入值Search 放入模型model當中
            Data.Search = Search;
            // 新增頁面模型的分頁
            Data.Paging = new ForPaging(Page);
            // 從Service中取得頁面所需陣列資料
            Data.DataList = guestbookDBservice.GetDataList(Data.Paging,Data.Search);
            return View(Data);
        }

        /// <summary>
        /// 新增留言一開始載入畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            // 部分檢視
            return PartialView();
        }

        // 新增留言
        [HttpPost]
        // 使用Bind的Include來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Add([Bind(Include ="Name,Content")]BU_GUESTBOOK Data)
        {
            //使用Service來新增一筆資料
            guestbookDBservice.InsertGuestbooks(Data);

            return RedirectToAction("Index");
        }

        #region 修改留言
        public ActionResult Edit(int Id)
        {
            // 取得頁面所需資料，藉由Service取得
            BU_GUESTBOOK Data = guestbookDBservice.GetDataById(Id);

            return View(Data);
        }
        
        [HttpPost]
        // 使用Bind的Include來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Edit(int Id,[Bind(Include = "Name,Content")]BU_GUESTBOOK UpdateData)
        {
            // 是否可修改判斷
            if (guestbookDBservice.CheckUpdate(Id))
            {
                // 將編號設定至修改資料中
                UpdateData.id = Id;
                // 使用Service來修改資料
                guestbookDBservice.UpdateGuestbooks(UpdateData);
                return RedirectToAction("Index");
            }
            else
            {
                //重新導向開始頁面
                return RedirectToAction("Index");
            }
            
        }
        #endregion

        #region 回覆留言
        public ActionResult Reply(int Id)
        {
            //取得頁面所需資料，藉由Service取得
            BU_GUESTBOOK Data = guestbookDBservice.GetDataById(Id);
            //將資料傳入View中
            return View(Data);
        }

        [HttpPost]
        // 使用Bind的Include來定義只接受的欄位，用來避免傳入其他不相干值
        public ActionResult Reply(int Id, [Bind(Include = "Reply,ReplyTime")]BU_GUESTBOOK ReplyData)
        {
            // 是否可修改判斷
            if (guestbookDBservice.CheckUpdate(Id))
            {
                // 將編號設定至修改資料中
                ReplyData.id = Id;
                // 使用Service來修改資料
                guestbookDBservice.ReplyGuestbooks(ReplyData);
                return RedirectToAction("Index");
            }
            else
            {
                //重新導致開始頁面
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region
        //刪除頁面要根據傳入編號來刪除資料
        public ActionResult Delete(int Id)
        {
            //使用Service來刪除資料
            guestbookDBservice.DeleteGuestbooks(Id);
            //重新導向頁面至開始頁面
            return RedirectToAction("Index");
        }
        #endregion
    }
}