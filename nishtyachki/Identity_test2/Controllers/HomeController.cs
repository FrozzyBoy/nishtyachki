﻿using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class HomeController : MvcControlWithBaseUrl
    {        
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Url = BaseUrl;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Url = BaseUrl;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Url = BaseUrl;

            return View();
        }
    }
}