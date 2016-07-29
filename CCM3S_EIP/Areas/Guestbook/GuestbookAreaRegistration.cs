using System.Web.Mvc;

namespace CCM3S_EIP.Areas.Guestbook
{
    public class GuestbookAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Guestbook";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Guestbook_default",
                "Guestbook/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}