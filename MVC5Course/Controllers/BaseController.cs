using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
        protected FabricsEntities db = new FabricsEntities();

        protected override void HandleUnknownAction(string actionName)
        {
            //base.HandleUnknownAction(actionName);

            //Controller 名稱只能用原本的去展現，若使用Route會出現錯誤
            this.RedirectToAction("Index").ExecuteResult(this.ControllerContext);
        }
    }
}