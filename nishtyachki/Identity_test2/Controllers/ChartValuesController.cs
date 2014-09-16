using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public object Get(int count)
        {
            TempData data = new TempData();

            data.numbers = new long[count];
            data.labels = new string[count];

            List<UserStats> userStat = null;

            using (var context = new AppDbContext())
            {
                userStat = context.UserStats.ToList<UserStats>();
            }

            count = Math.Min(userStat.Count, count);

            for (int i = 0; i < count; i++)
            {
                data.labels[i] = userStat[i].UserName;
                data.numbers[i] = 0;
                foreach (var item in userStat[i].Stats)
                {
                    if (item.NewState == Queue.UserCurrentState.InQueue)
                    {
                        data.numbers[i]++;
                    }
                }
            }

            return data;
        }
        
        [Route("user/{userID}")]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public object Get(string userID)
        {
            UserStats stats = null;
            using (var context = new AppDbContext())
            {
                stats = (from st in context.UserStats
                             where st.UserName == userID
                             select st).FirstOrDefault<UserStats>();
            }

            var data = new TempData();

            var names = Enum.GetNames(typeof(AdminApp.Queue.UserCurrentState));

            int length = names.Length;

            data.numbers = new long[length];
            data.labels = new string[length];

            var sortedStats =stats.Stats.ToArray<Stats>();
            Array.Sort<Stats>(sortedStats, (x, y) =>
            {
                return x.NewState.CompareTo(y.NewState);
            });

            int labelsCount = -1;

            for (int i = 0; i < sortedStats.Length; i++)
            {
                data.labels[++labelsCount] = names[i];
                while (names[i] == data.labels[labelsCount])
                {
                    data.numbers[labelsCount]++;
                }
            }

            return data;
        }
    }
}