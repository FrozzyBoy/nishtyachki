using AdminApp.Models;
using System.Collections.Generic;
using System.Web.Http;
using System;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/UserInfo")]
    public class UserInfoValuesController : ApiController
    {        
        // GET api/<controller>
        [Route("")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public IEnumerable<UserInfo> Get()
        {
            return AdminApp.Queue.User.GetUserInfo();
        }

        // GET api/<controller>/5
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public UserInfo Get(string id)
        {
            return AdminApp.Queue.User.GetUserInfo(id);
        }

        // POST api/<controller>
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}