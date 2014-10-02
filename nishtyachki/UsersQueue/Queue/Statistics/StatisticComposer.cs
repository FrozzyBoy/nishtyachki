using UsersQueue.Services.TransferObjects;
using System.Collections.Generic;
using UsersQueue.Queue.UserInformtion;
using System;
using UsersQueue.Model;
using System.Linq;

namespace UsersQueue.Queue.Statistics
{
    public static class StatisticComposer
    {
        public static ChartValues ChartPersonalStatistic(string userId)
        {
            string[] lbls = Enum.GetNames(typeof(UserCurrentState));
            int[] nums = new int[lbls.Length];

            var info = UserStats.GetUserStat(userId).StatsForUser;

            foreach (var item in info)
            {
                nums[(int)item.NewState]++;
            }

            ChartValues result = new ChartValues() { labels = lbls, numbers = nums };
            return result;
        }

        public static ChartValues GeneralWasMoreThenAthoresInState(UserCurrentState stat)
        {
            ChartValues result = new ChartValues();

            using (var context = new AppDbContext())
            {
                var search = (from ui in context.UsersInfo
                              select ui.UserName).ToList<string>();

                var listOfStatistics = new List<UserStats>();

                foreach (var name in search)
                {
                    var ust = new UserStats(name);
                    listOfStatistics.Add(ust);
                }

                listOfStatistics.Sort(
                    (x, y) => 
                        x.CountWasInState(stat).CompareTo(
                        y.CountWasInState(stat)));

                int length = listOfStatistics.Count;

                int[] count = new int[length];
                string[] labels = new string[length];

                for (int i = 0; i < length; i++)
                {
                    count[i] = listOfStatistics[i].CountWasInState(stat);
                    labels[i] = listOfStatistics[i].UserName;
                }

                result = new ChartValues() { labels = labels, numbers = count };
            }

            return result;
        }
    }
}
