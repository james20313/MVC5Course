﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [產生ViewBag下有一個Message可以用]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            //throw new AggregateException("Error");

            return View();
        }

        [LocalOnly]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}