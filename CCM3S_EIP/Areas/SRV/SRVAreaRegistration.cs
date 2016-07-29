using System.Web.Mvc;

namespace CCM3S_EIP.Areas.SRV
{
    public class SRVAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SRV";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SRV_default",
                "SRV/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}