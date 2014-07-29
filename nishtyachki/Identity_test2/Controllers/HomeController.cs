using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class HomeController : Controller
    {
        private string BaseUrl
        {
            get
            {
                return Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port) + Url.Content("~");
            }
        }

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