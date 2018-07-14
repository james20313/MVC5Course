using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : BaseController
    {
        // GET: MB
        public ActionResult Index()
        {
            ViewData.Model = "Hello Word";


            return View();
        }

        public ActionResult ViewBagDemo()
        {
            ViewBag.Text = "Test";
            return View();
        }

        public ActionResult ViewDataDemo()
        {
            ViewData["Text"] = "Test";
            return View();
        }

        public ActionResult TempdataSave()
        {
            TempData["Text"] = "Temp";

            return RedirectToAction("TempDataDemo");
        }

        public ActionResult TempDataDemo()
        {
            TempData["Text"] = "Test";
            return View();
        }
    }
}