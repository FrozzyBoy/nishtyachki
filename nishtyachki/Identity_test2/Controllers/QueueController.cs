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
        [HttpGet]
        public object Get()
        {
            return _channel.GetQueueInstance();
        }
                
        [Route("delete/{id}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Delete(string id)
        {
            _channel.DeleteUserByAdmin(id);
            return Ok();
        }

        [Route("change/{id}/role/{role}")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult ChangeUserRole(string id, int role)
        {
            _channel.ChangeUserRole(id, role);
            return Ok();
        }

        [Route("block")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult SwitchQueueState()
        {
            _channel.SwitchQueueState();
            return Ok();
        }

        [Route("update/queue")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult PushQueue(string[] userNames)
        {
            _channel.UpdateUsersInQueue(userNames);
            return Ok();
        }

        [Route("sendMsg")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult SendMsg(object[] data)
        {
            string msg = data[0] as string;
            string id = data[1] as string;

            _channel.SendMsg(msg, id);
            return Ok();
        }
        
    }
}
