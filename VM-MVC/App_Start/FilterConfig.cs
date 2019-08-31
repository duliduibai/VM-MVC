using System;
using System.Web;
using System.Web.Mvc;

namespace VM_MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyActionFilterAttribute());
        }
    }

    public class AutheticateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.RequestContext.HttpContext;
            //if (filterContext.HttpContext.Request.RawUrl.Contains("Login"))
            //{
            //    return;
            //}
            var cookie = httpContext.Request.Cookies["vm_login"];
            var sessoinId = "";
            if (cookie != null)
            {
                sessoinId = cookie["session_id"];
            }
            var session = httpContext.Session[sessoinId];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult("Login", null);
            }
        }
    }
}
