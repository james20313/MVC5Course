using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewTest()
        {
            String model = "My Data";
            return View((object)model);
        }

        public ActionResult PartialViewTest()
        {
            String model = "Partial My Data";
            return PartialView("ViewTest",(object)model);
        }
    }
}