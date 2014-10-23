using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class ChartController : MvcControlWithBaseUrl
    {
        public ActionResult Index()
        {
            ViewBag.Url = BaseUrl;
            return View();
        }
	}
}