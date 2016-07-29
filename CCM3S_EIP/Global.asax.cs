using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CCM3S_EIP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // 避免 => 具有潛在危險 Request.Path 的值已從用戶端 (&) 偵測到。
        protected void Application_Error()
        {
            if (Server.GetLastError().GetType().ToString() == "System.Web.HttpRequestValidationException")
            {
                Response.Write("HttpRequestValidationException");
            }

            //Server.ClearError();
        }
    }
}
