using AdminApp.QueueChannel;
using System;
using System.Web.Http;

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
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            _channel.DeleteUserByAdmin(id);
            return Ok();
        }

        [Route("change/{id}/role/{role}")]
        [AllowAnonymous]
        [HttpPut]
        public IHttpActionResult ChangeUserRole(string id, int role)
        {
            _channel.ChangeUserRole(id, role);
            return Ok();
        }

        [Route("block")]
        [AllowAnonymous]
        [HttpPut]
        public IHttpActionResult SwitchQueueState()
        {
            _channel.SwitchQueueState();
            return Ok();
        }

        [Route("update/queue")]
        [AllowAnonymous]
        [HttpPut]
        public IHttpActionResult UpdateUsersQueue(string[] userNames)
        {
            _channel.UpdateUsersInQueue(userNames);
            return Ok();
        }

        [Route("sendMsg")]
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult SendMsg(object[] data)
        {
            string msg = data[0] as string;
            string id = data[1] as string;

            IHttpActionResult result = null;

            if (msg == null || id == null)
            {
                var msgError = msg == null ? "msg parametr is not valid" : "";
                var idError = id == null ? "id parametr is not valid" : "";

                var errors = string.Join(" ", msgError, idError);

                result = BadRequest(errors);
            }
            else
            {
                try
                {
                    _channel.SendMsg(msg, id);
                    result = Ok();
                }
                catch (System.ServiceModel.CommunicationException)
                {
                    result = InternalServerError();
                }
                catch (Exception)
                {
                    result = NotFound();
                }
            }

            return result;
        }
        
    }
}
