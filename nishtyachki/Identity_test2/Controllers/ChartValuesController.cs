using AdminApp.Models;
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
        public object Get(int count)
        {
            var list = new List<UserInfo>();
            list.AddRange(AdminApp.Queue.User.GetUserInfo());

            var stats = new List<Stats>();
            foreach (var item in list)
            {
                stats.AddRange(item.Stats);
            }

            stats.Sort((lh, rh) =>
                {
                    return lh.TimeOfStayingInQuee.Seconds.CompareTo(rh.TimeOfStayingInQuee.Seconds);
                });

            stats.Reverse();

            if (stats.Count < count)
            {
                count = stats.Count;
            }

            TempData data = new TempData();

            data.numbers = new long[count];
            data.labels = new string[count];

            for (int i = 0; i < count; i++)
            {
                data.numbers[i] = stats[i].TimeOfStayingInQuee.Seconds;
                data.labels[i] = stats[i].TimeOfBeginToStayInQuee.ToString();
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