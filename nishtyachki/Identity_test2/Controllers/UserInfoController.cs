using AdminApp.AdminAppService;
using AdminApp.QueueChannel;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class UserInfoController : MvcControlWithBaseUrl
    {
        private readonly IQueueChannel _channel;

        public UserInfoController(IQueueChannel channel)
        {
            _channel = channel;
        }

        // GET: /UserInfo/
        public ActionResult Index(int state = 3)
        {
            ViewBag.Url = BaseUrl;
            var model = _channel.GetAllUsersInfo();
            return View(model);
        }

        public ActionResult UserPage(string userId)
        {
            ViewBag.Url = BaseUrl;
            UserInfo user = _channel.GetUserInfoByID(userId);
            return View(user);
        }
        
	}
}