using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class UserInfoController : MvcControlWithBaseUrl
    {        
        // GET: /UserInfo/
        public ActionResult Index(string userID)
        {
            ViewBag.Url = BaseUrl;
            ViewBag.ID = userID;
            return View();
        }
        
	}
}