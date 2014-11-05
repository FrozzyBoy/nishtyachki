﻿using System;
using System.Collections.Generic;
using System.Linq;
using UsersQueue.Model;
using UsersQueue.Queue.UserInformtion;
using UsersQueue.Services.TransferObjects;

namespace UsersQueue.Queue.Statistics
{
    public static class StatisticComposer
    {
        public static ChartValues ChartPersonalStatistic(string userId)
        {
            List<string> lbls = new List<string>();

            string[] collection = Enum.GetNames(typeof(UserCurrentState));

            foreach (var item in collection)
            {
                lbls.Add(item);
            }

            List<int> nums = new List<int>(lbls.Count);

            for (int i = 0; i < lbls.Count; i++)
            {
                nums.Add(0);
            }

            var info = UserStats.GetUserStat(userId).StatsForUser;

            foreach (var item in info)
            {
                nums[(int)item.NewState]++;
            }

            lbls.RemoveRange(0, 2);
            nums.RemoveRange(0, 2);

            ChartValues result = new ChartValues() { labels = lbls.ToArray(), numbers = nums.ToArray() };
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
                        y.CountWasInState(stat).CompareTo(
                        x.CountWasInState(stat)));

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

        internal static ChartValues GetStatisticsForNishtiak(string nishtiakID)
        {
            NishtiakLogs[] all = null;

            //using (var context = new AppDbContext())
            //{
                NishtiakTransferObject nishtiak = Nishtiachki.Nishtiachok.GetNishtiachokByName(nishtiakID).GetNishtiakTransferObject();// context.Nishtiaki.SingleOrDefault<NishtiakTransferObject>(x => x.ID == nishtiakID);
                all = nishtiak.AllChanges.ToArray<NishtiakLogs>();
            //}

            List<string> names = new List<string>();
            List<int> counts = new List<int>();

            if (all.Length > 0)
            {
                string lastName = "";

                for (int i = 1; i < all.Length + 1; i++)
                {
                    int count = 1;
                    lastName = all[i - 1].ChangeWas;

                    while (i < all.Length && all[i].ChangeWas == lastName)
                    {
                        i++;
                        count++;
                    }

                    counts.Add(count);
                    names.Add(lastName);
                }
            }
            else
            {
                counts.Add(0);
                names.Add("no one");
            }

            ChartValues result = new ChartValues() { labels = names.ToArray(), numbers = counts.ToArray() };
            return result;


        }
    }
}
