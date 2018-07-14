using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class 產生ViewBag下有一個Message可以用Attribute : ActionFilterAttribute
    {
        //通常應用在：進入Action前檢查用途
        //關注點分離
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "Your application description page.";

            base.OnActionExecuting(filterContext);
        }
    }
}