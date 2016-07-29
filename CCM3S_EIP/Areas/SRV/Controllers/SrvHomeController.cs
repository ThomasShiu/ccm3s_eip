using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using CCM3S_EIP.Models;
using CCM3S_EIP.Areas.SRV.ViewModels;
using CCM3S_EIP.App_Start;

/*  參考資料
 *  http://www.aizhengli.com/entity-framework6-mvc5-started/111/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application.html
 * 
 * */
namespace CCM3S_EIP.Areas.SRV.Controllers
{
    public class SrvHomeController : Controller
    {
        // GET: SRV/Home
        private EIP01Entities db = new EIP01Entities();
        //private int pageSize = 18;

        // GET: MIS/IP_Address
        [UserTraceLog]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            
            //排序
            ViewBag.ProdnoSortParm = string.IsNullOrEmpty(sortOrder) ? "prodno_desc" : "";
            ViewBag.TypeSortParm = sortOrder == "type_asc" ? "type_desc" : "type_asc";
            ViewBag.ItemSortParm = sortOrder == "item_asc" ? "item_desc" : "item_asc";
            ViewBag.ItemNmSortParm = sortOrder == "itemnm_asc" ? "itemnm_desc" : "itemnm_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var srvprodmt = from s in db.V_SRVPRODMT
                            select s;
            //搜尋
            if (!string.IsNullOrEmpty(searchString))
            {
                srvprodmt = srvprodmt.Where(s => s.ITEM_NO.ToUpper().Contains(searchString.ToUpper())
                    || s.PROD_NO.ToUpper().Contains(searchString.ToUpper())
                    || s.ITEM_NM.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                //排序
                case "prodno_desc": //產品號碼
                    srvprodmt = srvprodmt.OrderByDescending(s => s.PROD_NO);
                    break;
                case "type_desc"://型式
                    srvprodmt = srvprodmt.OrderByDescending(s => s.PROD_TY);
                    break;
                case "type_asc"://型式
                    srvprodmt = srvprodmt.OrderBy(s => s.PROD_TY);
                    break;
                case "item_desc"://品號
                    srvprodmt = srvprodmt.OrderByDescending(s => s.ITEM_NO);
                    break;
                case "item_asc"://品號
                    srvprodmt = srvprodmt.OrderBy(s => s.ITEM_NO);
                    break;
                case "itemnm_desc"://品名
                    srvprodmt = srvprodmt.OrderByDescending(s => s.ITEM_NM);
                    break;
                case "itemnm_asc"://品名
                    srvprodmt = srvprodmt.OrderBy(s => s.ITEM_NM);
                    break;
                default:
                    srvprodmt = srvprodmt.OrderBy(s => s.PROD_NO);
                    break;
            }
            int pageSize = 16;
            int pageNumber = (page ?? 1);
            
            //var srvprodmt = db.SRVPRODMT.OrderBy(x => x.PROD_NO);
            var result = srvprodmt.ToPagedList(pageNumber, pageSize);

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

        // GET: Categories
        //public async Task<ActionResult> Index(int page = 1)
        //{
        //    return View(await db.SRVPRODMT.Take(10).ToListAsync());
        //}

        // GET: Categories/Details/5
        public async Task<ActionResult> DetailsViewModel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SRVPRODMT srvprodmt = await db.SRVPRODMT.FindAsync(id);
            if (srvprodmt == null)
            {
                return HttpNotFound();
            }

            if (id.HasValue)
            {
                var srvprodmt_tb = db.SRVPRODMT.FirstOrDefault(x => x.id == id.Value);
                var srvproddl_tb = db.SRVPRODDL.Where(x => x.PROD_NO == srvprodmt_tb.PROD_NO).OrderBy(x => x.PROD_SR).ToList();
                var v_srvdatamt_tb = db.V_SRVDATAMT.Where(x => x.PROD_NO == srvprodmt_tb.PROD_NO).OrderBy(x => x.VCH_NO).ToList();
                SrvProdViewModel viewModel = new SrvProdViewModel();
                viewModel.srvprodmt_data = srvprodmt_tb;
                viewModel.srvproddl_list = srvproddl_tb;
                viewModel.vsrvdatamt_list = v_srvdatamt_tb;
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }
    }
}