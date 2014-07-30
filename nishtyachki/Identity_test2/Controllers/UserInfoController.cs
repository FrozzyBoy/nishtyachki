using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminApp.Queue;

namespace AdminApp.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult User(string id)
        {
            return View();
        }
	}
}