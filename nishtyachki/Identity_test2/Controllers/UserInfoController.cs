using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Queue;
using System.Text;

namespace AdminApp.Controllers
{
    public class UserInfoController : Controller
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

        //
        // GET: /UserInfo/
        public ActionResult Index()
        {
            ViewBag.Url = BaseUrl;
            return View();
        }
        public ActionResult User(string id)
        {
            ViewBag.Url = BaseUrl;
            ViewBag.ID = id;
            return View();
        }
	}
}