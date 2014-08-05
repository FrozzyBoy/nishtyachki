using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class ChartController : Controller
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

        public ActionResult Index()
        {
            ViewBag.Url = BaseUrl;
            return View();
        }
	}
}