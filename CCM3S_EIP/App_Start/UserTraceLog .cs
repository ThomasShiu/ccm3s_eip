using CCM3S_EIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CCM3S_EIP.App_Start
{
    public class UserTraceLog : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.UrlReferrer == null)
                return;
            //ActionLogRepository ActionLog = new ActionLogRepository();
            EIP01Entities db = new EIP01Entities();
            ActionLog NewLog = new ActionLog()
            {
                Refer = filterContext.HttpContext.Request.UrlReferrer.AbsolutePath,
                Destination = filterContext.HttpContext.Request.Url.AbsolutePath,
                Method = filterContext.HttpContext.Request.HttpMethod,
                RequestTime = DateTime.Now,
                IPAddress = filterContext.HttpContext.Request.UserHostAddress,
                Operator = (HttpContext.Current.User.Identity.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "Anonymous")
            };
            db.ActionLog.Add(NewLog);
            db.SaveChanges();
            base.OnActionExecuting(filterContext);
        }
    }
}
