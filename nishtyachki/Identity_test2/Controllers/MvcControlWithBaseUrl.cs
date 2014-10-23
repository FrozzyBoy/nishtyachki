using System.Text;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class MvcControlWithBaseUrl : Controller
    {
        protected string BaseUrl
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
    }
}