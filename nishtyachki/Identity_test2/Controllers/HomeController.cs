﻿using Microsoft.Practices.Unity;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
    [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class HomeController : Controller
    {
        private string BaseUrl
        {
            get
            {
                StringBuilder sb = new StringBuilder("");

                sb.Append(Request.Url.Scheme);
                sb.Append(System.Uri.SchemeDelimiter);
                sb.Append(Request.Url.Host);
                sb.Append(Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);

                string content = Url.Content("~");
                sb.Append(content == "" ? "/" : content);

                return sb.ToString();
            }
        }
                
        [Authorize]
        [OutputCache(NoStore=true, Duration=0, VaryByParam="None")]
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