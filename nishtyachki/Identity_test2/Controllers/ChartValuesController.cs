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
        public IEnumerable<int[]> Get()
        {
            Random rnd = new Random();
            int length = 1000;

            int count = int.MaxValue;

            int[][] arr = new int[length][];

            DateTime dt = DateTime.Now;
            for (int i = 0; i < length; i++)
            {
                arr[i] = new int[4];
                arr[i][0] = rnd.Next(count);

                dt = dt.AddDays(rnd.Next(1,3)); //new DateTime(rnd.Next(1, count));

                arr[i][1] = dt.Year;
                arr[i][2] = dt.Month;
                arr[i][3] = dt.Day;
            }

            return arr;
        }
    }
}