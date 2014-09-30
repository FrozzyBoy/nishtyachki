using System.Web.Http;
using AdminApp.QueueChannel;
using System.Collections.Generic;
using AdminApp.AdminAppService;

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
        public object Get()
        {
            return _channel.GetQueueInstance();
        }
                
        [Route("delete/{id}")]
        [AllowAnonymous]
        public void Delete(string id)
        {
            _channel.DeleteUserByAdmin(id);
        }

        [Route("change/{id}/role/{role}")]
        [AllowAnonymous]
        public void ChangeUserRole(string id, int role)
        {
            _channel.ChangeUserRole(id, role);
        }

        [Route("block")]
        [AllowAnonymous]
        public void SwitchQueueState()
        {
            _channel.SwitchQueueState();
        }

        [Route("update/queue")]
        [AllowAnonymous]
        public void PushQueue(string[] userNames)
        {
            _channel.UpdateUsersInQueue(userNames);
        }

        [Route("sendMsg")]
        [AllowAnonymous]
        public void SendMsg(object[] data)
        {
            string msg = data[0] as string;
            string id = data[1] as string;

            _channel.SendMsg(msg, id);
        }
        
    }
}
