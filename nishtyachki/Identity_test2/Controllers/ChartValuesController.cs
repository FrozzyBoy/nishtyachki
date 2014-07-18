using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminApp.Controllers
{
    [RoutePrefix("api/chartValues")]
    public class ChartValuesController : ApiController
    {
        // GET api/<controller>
        [Route("")]
        [AllowAnonymous]
        public IEnumerable<object[]> Get()
        {
            Random rnd = new Random();
            int length = 10000;

            int count = int.MaxValue;

            object[][] arr = new object[length][];

            for (int i = 0; i < length; i++)
            {
                arr[i] = new object[2];
                arr[i][0] = new DateTime(rnd.Next(count));
                arr[i][1] = rnd.Next(0, count);
            }

            return arr;
        }
    }
}