using System.Text;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
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