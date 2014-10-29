using System.Web.Mvc;

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
            ViewBag.Message = "Nishtiachki application description page.";
            ViewBag.Url = BaseUrl;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";
            ViewBag.Url = BaseUrl;

            return View();
        }
    }
}