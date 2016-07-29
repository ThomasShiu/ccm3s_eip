using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CCM3S_EIP.Models;
using PagedList;
using PagedList.Mvc;
using CCM3S_EIP.Areas.MIS.Services;
using CCM3S_EIP.App_Start;

namespace CCM3S_EIP.Areas.MIS.Controllers
{
    public class IP_AddressController : Controller
    {
        IP_AddressCUIDServices ipaddrservice = new IP_AddressCUIDServices();
        EIP01Entities db = new EIP01Entities();
        //private int pageSize = 18;

        // GET: MIS/IP_Address
        [UserTraceLog]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var ip_address_tb = from s in db.IP_Address
                                select s;

            //搜尋
            if (!string.IsNullOrEmpty(searchString))
            {
                ip_address_tb = ip_address_tb.Where(s => s.ip_addr.ToUpper().Contains(searchString.ToUpper())
                    || s.mac_addr.ToUpper().Contains(searchString.ToUpper()));
            }
            ip_address_tb = ip_address_tb.OrderBy(s=>s.ip_addr);

            int pageSize = 16;
            int pageNumber = (page ?? 1);

            //var srvprodmt = db.SRVPRODMT.OrderBy(x => x.PROD_NO);
            var result = ip_address_tb.ToPagedList(pageNumber, pageSize);

            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 1; i <= result.PageCount; i++)
            {
                items.Add(new SelectListItem(){
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = i.Equals(result.PageNumber)
                });
            }
            ViewBag.PageItems = items;
            //頁數資訊
            ViewBag.PageCountAndCurrentLocation = string.Format(
                "第{0}頁 / 共{1}頁",
                result.PageNumber,result.PageCount);
            //資料列數資訊
            ViewBag.ItemSliceAndTotalFormat = string.Format(
                "顯示項目：{0} ~ {1},共{2}項",
                result.FirstItemOnPage,
                result.LastItemOnPage,
                result.TotalItemCount);

            return View(result);
        }
        //public async Task<ActionResult> Index(int page = 1)
        //{
        //    int currentPage = page < 1 ? 1 : page;
        //    var customers = db.IP_Address.OrderBy(x => x.ip_addr);
        //    var result = customers.ToPagedList(currentPage, pageSize);

        //    return View(result);
        //    //return View(await db.IP_Address.ToListAsync());
        //}


        // GET: MIS/IP_Address/Details/5
        [UserTraceLog]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IP_Address iP_Address = await db.IP_Address.FindAsync(id);
            if (iP_Address == null)
            {
                return HttpNotFound();
            }
            return View(iP_Address);
        }

        // GET: MIS/IP_Address/Create
        [UserTraceLog]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MIS/IP_Address/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserTraceLog]
        public async Task<ActionResult> Create([Bind(Include = "sn,ip_addr,mac_addr,dept,user,device,os,group,antivirus,remark")] IP_Address iP_Address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.IP_Address.Add(iP_Address);
                    await db.SaveChangesAsync();
                }
                catch(Exception)
                {

                }
                
                return RedirectToAction("Index");
            }

            return View(iP_Address);
        }

        // GET: MIS/IP_Address/Edit/5
        [UserTraceLog]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IP_Address iP_Address = await db.IP_Address.FindAsync(id);
            if (iP_Address == null)
            {
                return HttpNotFound();
            }
            return View(iP_Address);
        }

        // POST: MIS/IP_Address/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [UserTraceLog]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "sn,ip_addr,mac_addr,dept,user,device,os,group,antivirus,remark")] IP_Address iP_Address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iP_Address).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(iP_Address);
        }

        // GET: MIS/IP_Address/Delete/5
        [UserTraceLog]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IP_Address iP_Address = await db.IP_Address.FindAsync(id);
            if (iP_Address == null)
            {
                return HttpNotFound();
            }
            return View(iP_Address);
        }

        // POST: MIS/IP_Address/Delete/5
        [UserTraceLog]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IP_Address iP_Address = await db.IP_Address.FindAsync(id);
            db.IP_Address.Remove(iP_Address);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
