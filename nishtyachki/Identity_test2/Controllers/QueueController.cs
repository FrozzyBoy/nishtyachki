using System.Web.Http;
using AdminApp.Queue;
using AdminApp.QueueChannel;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/queue")]
    public class QueueController : ApiController
    {
        private readonly IQueueChannel _channel;

        public QueueController(IQueueChannel channel)
        {
            _channel = channel;
        }

        // GET api/<controller>
        [Route("")]
        [AllowAnonymous]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public UsersQueue Get()
        {
            return UsersQueue.Instance;
        }
       
        [HttpDelete]
        [Route("delete/{id}")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Delete(string id)
        {
            UsersQueue.Instance.DeleteUserByAdmin(UsersQueue.Instance.GetUser(id));
        }

        [Route("change/{id}/role/{role}")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void ChangeUserRole(string id, int role)
        {
            UsersQueue.Instance.ChangeRoleByAdmin(UsersQueue.Instance.GetUser(id), (Role)role);
        }

        [Route("block")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void SwitchQueueState()
        {
            UsersQueue.SwitchQueueState();
        }

        [Route("update/queue")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void PushQueue(string[] userNames)
        {
            UsersQueue.Instance.UpdateQueue(userNames);
        }

        [Route("sendMsg")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void SendMsg(object[] data)
        {
            string msg = data[0] as string;
            string id = data[1] as string;

            //UsersQueue.Instance.GetUser(id).Client.ShowMessage(msg);
        }
        
    }
}
