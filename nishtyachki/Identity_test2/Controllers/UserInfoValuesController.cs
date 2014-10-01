using System.Collections.Generic;
using System.Web.Http;
using System;
using AdminApp.QueueChannel;
using AdminApp.AdminAppService;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/UserInfo")]    
    public class UserInfoValuesController : ApiController
    {
        private readonly IQueueChannel _channel;

        public UserInfoValuesController(IQueueChannel channel)
        {
            _channel = channel;
        }

        // GET api/<controller>
        [Route("")]
        [HttpGet]
        public IEnumerable<UserInfo> Get()
        {
            return _channel.GetAllUsersInfo();
        }

        // GET api/<controller>/5
        [Route("")]
        [HttpGet]
        public UserInfo Get(string id)
        {
            return _channel.GetUserInfoByID(id);
        }

    }
}