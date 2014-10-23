using AdminApp.AdminAppService;
using AdminApp.QueueChannel;
using System.Web.Mvc;

namespace AdminApp.Controllers
{
    public class StatisticViewController : MvcControlWithBaseUrl
    {
        private readonly IQueueChannel _channel;

        public StatisticViewController(IQueueChannel channel)
        {
            _channel = channel;
        }

        // GET: /StatisticView/
        public ActionResult Index()
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

        public ActionResult NishtiakPage(string nishtiakId)
        {
            ViewBag.Url = BaseUrl;
            Nishtiachok nisht = _channel.GetNishtiakById(nishtiakId);
            return View(nisht);
        }

	}
}