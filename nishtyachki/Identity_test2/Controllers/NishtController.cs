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
            NishtiachkiContainer.AddNistiachok(new Nishtiachok(Nishtiachok_State.free, "1"));
            return NishtiachkiContainer.Nishtiachki;
            
        }
    }
}
