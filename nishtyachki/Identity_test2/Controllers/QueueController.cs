using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminApp.Queue;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/queue")]
    public class QueueController : ApiController
    {
        // GET api/<controller>
        [Route("")]
        [AllowAnonymous]
        public IEnumerable<User> Get()
        {
            return UsersQueue._queue;
        }
       

        [Route("delete/{id}")]
        public void Delete(string id)
        {
            User user = new User();

            for (int i = 0; i < UsersQueue._queue.Count; i++)
            {
                if (UsersQueue._queue[i].ID == id)
                {
                    user = UsersQueue._queue[i];
                    break;
                }
            }

            UsersQueue.DeleteFromTheQueue(user);
        }

        [Route("change/{id}/srole/{role}")]
        public void DeleteSettings(string id, int role)
        {
            User user = new User();

            for (int i = 0; i < UsersQueue._queue.Count; i++)
            {
                if (UsersQueue._queue[i].ID == id)
                {
                    user = UsersQueue._queue[i];
                    break;
                }
            }

            UsersQueue.ChangeRoleByAdmin(user, (Role)role);
        }
         

    }
}
