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
           
            List<UserStats> userStat = new List<UserStats>();

            using (var context = new AppDbContext())
            {
                var userinfo = context.UsersInfo.ToList<UserInfo>();
                foreach (var item in userinfo)
                {
                    userStat.Add(new UserStats(item.UserName));
                }
            }

            count = Math.Min(userStat.Count, count);

            for (int i = 0; i < count; i++)
            {
                data.labels[i] = userStat[i].UserName;
                data.numbers[i] = 0;
                foreach (var item in userStat[i].StatsForUser)
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
            UserStats stats = new UserStats(userID);
            
            var names = Enum.GetNames(typeof(AdminApp.Queue.UserCurrentState));

            int length = names.Length;

            var data = new { labels = new string[length], numbers = new long[length] };

            for (int i = 0; i < length; i++)
            {
                data.labels[i] = names[i];
                data.numbers[i] = 0;
                for (int j = 0; j < stats.StatsForUser.Count; j++)
                {
                    if (names[i] == stats.StatsForUser[j].NewState.ToString())
                    {
                        data.numbers[i]++;
                    }
                }
            }

            return data;
        }
    }
}