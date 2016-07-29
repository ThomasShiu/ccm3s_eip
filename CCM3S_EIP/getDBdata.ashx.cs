using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCM3S_EIP
{
    /// <summary>
    /// getDBdata1 的摘要描述
    /// </summary>
    public class getDBdata1 : IHttpHandler
    {

        public EIP01Entities eip_db = new EIP01Entities();
        public CCM_MainEntities erp_db = new CCM_MainEntities();

        public void ProcessRequest(HttpContext context)
        {
            var v_key = context.Request.QueryString["key"];
            var v_switch = context.Request.QueryString["sw"];
            string v_str = "";
            switch (v_switch)
            {
                case "item_nm": //產品名稱
                    var result1 = from c in erp_db.ITEM
                                  where (c.ITEM_NO == v_key)
                                  select c;

                    foreach (var q in result1)
                    {
                        v_str = q.ITEM_NM;
                    }
                    break;
                case "item_sp"://產品規格
                    var result2 = from c in erp_db.ITEM
                                  where (c.ITEM_NO == v_key)
                                  select c;

                    foreach (var q in result2)
                    {
                        v_str = q.ITEM_SP;
                    }
                    break;
                case "short_nm"://客戶簡稱
                    var result3 = from c in erp_db.customers
                                  where (c.CS_NO == v_key)
                                  select c;

                    foreach (var q in result3)
                    {
                        v_str = q.SHORT_NM;
                    }
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(v_str);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}