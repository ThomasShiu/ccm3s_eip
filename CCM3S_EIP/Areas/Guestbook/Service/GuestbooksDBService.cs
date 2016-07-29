using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM3S_EIP.Areas.Guestbook.Service
{
    public class GuestbooksDBService
    {
        EIP01Entities db = new EIP01Entities();

        public List<BU_GUESTBOOK> GetDataList(ForPaging Paging, String Search)
        {
            //宣告要接受全部搜尋資料的方法
            IQueryable<BU_GUESTBOOK> SearchData;

            //判斷搜尋是否為空或NULL，用於決定要呼叫取得搜尋資料
            if (String.IsNullOrEmpty(Search))
            {
                //SearchData = db.BU_GUESTBOOK;
                SearchData = GetAllDataList(Paging);
            }
            else
            {
                SearchData = GetAllDataList(Paging,Search);
                //SearchData = db.BU_GUESTBOOK.Where(p => p.Name.Contains(Search)
                //    || p.Content.Contains(Search) || p.Reply.Contains(Search));
            }

            //先排序再根據分頁來回傳所需部分的資料陣列
            //return SearchData.ToList();
            return SearchData.OrderByDescending(p => p.id).Skip((Paging.NowPage - 1) * Paging.ItemNum).Take(Paging.ItemNum).ToList();
            
        }

        //無搜尋值的搜尋資料方法
        public IQueryable<BU_GUESTBOOK> GetAllDataList(ForPaging Paging)
        {
            //宣告要回傳的搜尋資料庫中的Guestbooks資料表
            IQueryable<BU_GUESTBOOK> Data = db.BU_GUESTBOOK;
            //計算所需頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            return Data;
        }

        //包含搜尋值的搜尋資料方法
        public IQueryable<BU_GUESTBOOK> GetAllDataList(ForPaging Paging,string Search)
        {
            //宣告要回傳的搜尋資料庫中的Guestbooks資料表
            IQueryable<BU_GUESTBOOK> Data = db.BU_GUESTBOOK.Where(p=>p.Name.Contains(Search)
            ||p.Content.Contains(Search)|| p.Reply.Contains(Search));

            //計算所需頁數
            Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
            //重新設定正確的頁數，避免有不正確值傳入
            Paging.SetRightPage();
            return Data;
        }


        #region 新增一筆資料
        public void  InsertGuestbooks(BU_GUESTBOOK newData)
        {
            //設定新增時間
            newData.CreateTime = DateTime.Now;
            //將資料加入資料庫實體
            db.BU_GUESTBOOK.Add(newData);
            //儲存資料庫變更
            db.SaveChanges();
        }
        #endregion

        #region 查詢一筆資料
        public BU_GUESTBOOK GetDataById(int id)
        {
           
            //回傳根據標號所取得的資料
            return db.BU_GUESTBOOK.Find(id);
        }
        #endregion


        #region 修改留言方法
        public void UpdateGuestbooks(BU_GUESTBOOK UpdateData)
        {
            //取得要修改的資料
            BU_GUESTBOOK OldDate = db.BU_GUESTBOOK.Find(UpdateData.id);
            //修改資料庫裡的值
            OldDate.Name = UpdateData.Name;
            OldDate.Content = UpdateData.Content;
            //儲存資料庫變更
            db.SaveChanges();

        }
        #endregion
        #region 回覆留言方法
        public void ReplyGuestbooks(BU_GUESTBOOK ReplyData)
        {
            //取得要修改的資料
            BU_GUESTBOOK OldData = db.BU_GUESTBOOK.Find(ReplyData.id);
            //設定回覆內容
            OldData.Reply = ReplyData.Reply;
            //設定回覆時間為現在
            OldData.ReplyTime = DateTime.Now;
            //儲存資料庫變更
            db.SaveChanges();
        }
        #endregion

        #region 檢查相關
        public bool CheckUpdate(int id)
        {
            //根據ID取得要修改的資料
            BU_GUESTBOOK Data = db.BU_GUESTBOOK.Find(id);
            //判斷回傳(判斷是否有資料以及是否回覆資料)
            return (Data != null && Data.ReplyTime == null);
        }
        #endregion

        #region 刪除資料
        //刪除資料方法
        public void DeleteGuestbooks(int Id)
        {
            //根據Id取得要刪除的資料
            BU_GUESTBOOK DeleteData = db.BU_GUESTBOOK.Find(Id);
            //從實體資料庫中刪除資料
            db.BU_GUESTBOOK.Remove(DeleteData);
            //儲存資料庫變更
            db.SaveChanges();
        }
        #endregion
    }

}
