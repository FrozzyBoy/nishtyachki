using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminApp.Nishtiachki;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/nisht")]
    public class NishtController : ApiController
    {
        // GET api/<controller>
        [Route("")]
        [AllowAnonymous]
        public IEnumerable<Nishtiachok> Get()
        {
            return Nishtiachok.Nishtiachki;           
        }

        [Route("add/{id}")]
        public void Post(string id)
        {
            Nishtiachok.AddNistiachokByAdmin(id);
        }

        [Route("delete/{id}")]
        public void Delete(string id)
        {
            Nishtiachok.DeleteNishtiachok(id);
        }

        [Route("change/{id}/state/{state}")]
        public void DeleteSettings(string id, int state)
        {
            Nishtiachok.GetNishtiachokByNamme(id).ChangeNishtState((Nishtiachok_State)state);
        }
    }
}
