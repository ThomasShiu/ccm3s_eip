using System.Web.Mvc;

namespace CCM3S_EIP.Areas.CHART
{
    public class CHARTAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chart";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chart_default",
                "Chart/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}