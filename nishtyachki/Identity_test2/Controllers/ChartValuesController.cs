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
        private class TempData
        {
            public string[] labels;
            public long[] numbers;
        }

        // GET api/<controller>
        [Route("{count}")]
        [AllowAnonymous]
        public object Get(int count)
        {
            TempData data = new TempData();

            data.numbers = new long[count];
            data.labels = new string[count];

            for (int i = 0; i < data.numbers.Length; i++)
            {
                data.numbers[i] = data.numbers.Length - i;
                data.labels[i] = "Abc" + i;
            }

            return data;
        }
        
        [Route("user/{userID}")]
        public object GetUserEveryDayStatistic(string userID)
        {
            var stats = AdminApp.Queue.User.GetUserInfo(userID).Stats;
            
            var data = new TempData();

            if (stats != null)
            {

                int length = stats.Count;

                data.labels = new string[length];
                data.numbers = new long[length];

                for (int i = 0; i < length; i++)
                {
                    data.labels[i] = stats[i].TimeOfBeginToStayInQuee.ToString();
                    data.numbers[i] = stats[i].TimeOfStayingInQuee.Seconds;
                }
            }

            return data;
        }
    }
}