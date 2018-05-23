using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Newcgq.Extends
{
    public class AdminBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var result = HttpContext.Session.GetInt32("UserId");
            var userType = HttpContext.Session.GetString("UserType");
            if (result == null || userType != "admin")
            {
                filterContext.Result = new RedirectResult("/Home/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
