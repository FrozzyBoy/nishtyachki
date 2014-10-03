using System;
using System.Collections.Generic;
using System.Web.Http;
using AdminApp.QueueChannel;
using AdminApp.AdminAppService;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/nisht")]        
    public class NishtController : ApiController
    {
        private readonly IQueueChannel _channel;

        public NishtController(IQueueChannel channel)
        {
            if(channel == null)
            {
                throw new ArgumentNullException("channel");
            }

            _channel = channel;
        }

        // GET api/<controller>
        [Route("")]
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Nishtiachok> Get()
        {
            var result = _channel.GetAllNishtiaks();
            return result;
        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddNishtiak()
        {
            _channel.AddNishtiak();
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            _channel.DeleteNishtiak(id);
            return Ok();
        }

        [Route("change/{id}/state/{state}")]
        [HttpPut]
        public IHttpActionResult ChangeNishtiakState(string id, int state)
        {
            _channel.ChangeNishtiakState(id, state);
            return Ok();
        }
    }
}
