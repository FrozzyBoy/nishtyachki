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
            return NishtiachkiContainer.Nishtiachki;            
        }

        [Route("add/{id}")]
        public void Post(int id)
        {
            Nishtiachok nisht = new Nishtiachok(Nishtiachok_State.free);
            nisht.ID = id;
            NishtiachkiContainer.AddNistiachok(nisht);
        }

        [Route("delete/{id}")]
        public void Delete(int id)
        {
            Nishtiachok nisht = new Nishtiachok(Nishtiachok_State.free);

            for (int i = 0; i < NishtiachkiContainer.Nishtiachki.Count; i++)
            {
                if (NishtiachkiContainer.Nishtiachki[i].ID == id)
                {
                    nisht = NishtiachkiContainer.Nishtiachki[i];
                    break;
                }
            }

            NishtiachkiContainer.DeleteNishtiachok(nisht);
        }

        [Route("change/{id}/state/{state}")]
        public void DeleteSettings(int id, int state)
        {
            Nishtiachok nisht = new Nishtiachok(Nishtiachok_State.free);

            for (int i = 0; i < NishtiachkiContainer.Nishtiachki.Count; i++)
            {
                if (NishtiachkiContainer.Nishtiachki[i].ID == id)
                {
                    nisht = NishtiachkiContainer.Nishtiachki[i];
                    break;
                }
            }

            NishtiachkiContainer.ChangeStatNishtiachok(nisht, (Nishtiachok_State)state);
        }
         

    }
}
