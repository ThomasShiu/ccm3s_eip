
using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;
using System.Web.Mvc;

namespace CCM3S_EIP.Areas.Album.Service
{
    class AlbumsDBService
    {
        EIP01Entities db = new EIP01Entities();

        #region 查詢一筆資料
        public V_EMP_ALBUM GetDataById(string emp_no)
        {
            //回傳根據標號所取得的資料
            return db.V_EMP_ALBUM.Find(emp_no);
        }
        #endregion

        #region 查詢員工資料
        public List<V_EMP_ALBUM> GetData(string searchString)
        {
            //var hr_employee = from s in db.HR_EMPLOYEE
            //                  select s;
            var hr_employee = from e in db.V_EMP_ALBUM
                              select e;
            //搜尋
            if (!string.IsNullOrEmpty(searchString))
            {
                hr_employee = hr_employee.Where(s => s.EMPLYNM.ToUpper().Contains(searchString.ToUpper())
                                             || s.EMPLYID.ToUpper().Contains(searchString.ToUpper())
                                             || s.DEPNM.ToUpper().Contains(searchString.ToUpper()));
            }

           
            //hr_employee = hr_employee.Where(s => s.EMP_NO.ToUpper().Contains(searchString.ToUpper())

            //回傳根據標號所取得的資料
            return hr_employee.ToList();
        }
        #endregion

        #region 查詢零件資料
        public List<PU_ALBUMS> GetPUAlbumsData(string searchString)
        {
            //var hr_employee = from s in db.HR_EMPLOYEE
            //                  select s;
            var pu_albums = from e in db.PU_ALBUMS
                              where e.parentId == 0
                              select e;
            //搜尋
            if (!string.IsNullOrEmpty(searchString))
            {
                pu_albums = pu_albums.Where(s => s.FileName.ToUpper().Contains(searchString.ToUpper())
                    || s.Descript.ToUpper().Contains(searchString.ToUpper())
                    || s.KindType1.ToUpper().Contains(searchString.ToUpper()));
            }
            //回傳根據標號所取得的資料
            return pu_albums.ToList();
        }
        #endregion

        #region 查詢零件明細資料
        public List<PU_ALBUMS> GetPUAlbumsDetailData(string searchString,int id)
        {
            //var hr_employee = from s in db.HR_EMPLOYEE
            //                  select s;
            var pu_albums = from e in db.PU_ALBUMS
                            where e.parentId == id
                            select e;

            //回傳根據標號所取得的資料
            return pu_albums.ToList();
        }
        #endregion

        #region 新增零件主檔
        public void InsertItemData(PU_ALBUMS newData)
        {
            try {

            //設定新增時間
            newData.CreateDate = DateTime.Now;
            //將資料加入資料庫實體
            db.PU_ALBUMS.Add(newData);
            //儲存資料庫變更
            db.SaveChanges();
            }catch(Exception ex)
            {
                string v_error = ex.ToString();
            }
        }
        #endregion
    }
}
