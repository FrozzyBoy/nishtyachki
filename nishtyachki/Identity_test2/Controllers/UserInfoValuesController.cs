using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/UserInfo")]
    public class UserInfoValuesController : ApiController
    {        
        // GET api/<controller>
        [Route("")]
        public IEnumerable<UserInfo> Get()
        {
            return AdminApp.Queue.User.GetUserInfo();
        }

        // GET api/<controller>/5
        public UserInfo Get(string id)
        {
            return AdminApp.Queue.User.GetUserInfo(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}