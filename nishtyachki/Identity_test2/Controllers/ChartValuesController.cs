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
        // GET api/<controller>
        [Route("{count}")]
        public object GetGeneral(int count)
        {
            var data = new { labels = new string[count], numbers = new long[count] };
           
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
        public object GetPersonal(string userID)
        {
            UserStats stats = null;
            using (var context = new AppDbContext())
            {
                stats = (from st in context.UserStats
                             where st.UserName == userID
                             select st).FirstOrDefault<UserStats>();
            }
            
            var names = Enum.GetNames(typeof(AdminApp.Queue.UserCurrentState));

            int length = names.Length;

            var data = new { labels = new string[length], numbers = new long[length] };

            for (int i = 0; i < length; i++)
            {
                data.labels[i] = names[i];
                data.numbers[i] = 0;
                for (int j = 0; j < stats.Stats.Count; j++)
                {
                    if (names[i] == stats.Stats[j].NewState.ToString())
                    {
                        data.numbers[i]++;
                    }
                }
            }

            return data;
        }
    }
}