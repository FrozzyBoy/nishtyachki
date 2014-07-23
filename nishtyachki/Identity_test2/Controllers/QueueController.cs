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
            return UsersQueue.Queue;
        }
       

        [Route("delete/{id}")]
        public void Delete(string id)
        {
            UsersQueue.Instance.DeleteFromTheQueue(UsersQueue.Instance.GetUser(id));
        }

        [Route("change/{id}/role/{role}")]
        public void DeleteSettings(string id, int role)
        {
            UsersQueue.Instance.ChangeRoleByAdmin(UsersQueue.Instance.GetUser(id), (Role)role);
        }
         

    }
}
