using System.Collections.Generic;
using System.Web.Http;
using AdminApp.Nishtiachki;
using AdminApp.QueueChannel;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/nisht")]
    [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
    public class NishtController : ApiController
    {
        private readonly IQueueChannel _channel;

        public NishtController(IQueueChannel channel)
        {
            _channel = channel;
        }

        // GET api/<controller>
        [Route("")]
        [AllowAnonymous]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public IEnumerable<Nishtiachok> Get()
        {
            return Nishtiachok.Nishtiachki;           
        }

        [Route("add/{id}")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Post(string id)
        {
            Nishtiachok.AddNistiachokByAdmin(id);
        }

        [Route("delete/{id}")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Delete(string id)
        {
            Nishtiachok.DeleteNishtiachok(id);
        }

        [Route("change/{id}/state/{state}")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void DeleteSettings(string id, int state)
        {
            Nishtiachok.GetNishtiachokByNamme(id).ChangeNishtState((Nishtiachok_State)state);
        }
    }
}
