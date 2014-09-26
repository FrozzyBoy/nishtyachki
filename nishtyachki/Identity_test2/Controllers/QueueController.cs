using System.Web.Http;
using AdminApp.QueueChannel;
using System.Collections.Generic;
using AdminApp.AdminAppService;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/queue")]
    [System.Web.Mvc.ValidateAntiForgeryToken]
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
        public List<QueueUser> Get()
        {
            return _channel.GetAllUsersInQueue();
        }
       
        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(string id)
        {
            _channel.DeleteUserByAdmin(id);
        }

        [Route("change/{id}/role/{role}")]
        public void ChangeUserRole(string id, int role)
        {
            _channel.ChangeUserRole(id, role);
        }

        [Route("block")]
        public void SwitchQueueState()
        {
            _channel.SwitchQueueState();
        }

        [Route("update/queue")]
        public void PushQueue(string[] userNames)
        {
            _channel.UpdateUsersInQueue(userNames);
        }

        [Route("sendMsg")]
        public void SendMsg(object[] data)
        {
            string msg = data[0] as string;
            string id = data[1] as string;

            _channel.SendMsg(msg, id);
        }
        
    }
}
