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
        public IEnumerable<Nishtiachok> Get()
        {
            return _channel.GetAllNishtiaks();
        }

        [Route("add/{id}")]
        public void Post(string id)
        {
            _channel.AddNishtiak(id);
        }

        [Route("delete/{id}")]
        public void Delete(string id)
        {
            _channel.DeleteNishtiak(id);
        }

        [Route("change/{id}/state/{state}")]
        public void ChangeUserRole(string id, int state)
        {
            _channel.ChangeUserRole(id, state);
        }
    }
}
