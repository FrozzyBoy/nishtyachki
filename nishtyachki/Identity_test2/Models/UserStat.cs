﻿using AdminApp.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Models
{
    public class UserStat
    {
        public static UserStat GetUserStat(string id)
        {
            return new UserStat();
        }

        private List<Stats> _stats;
        private UserStat(IEnumerable<Stats> stats = null)
        {
            _stats = new List<Stats>();
            if (stats != null)
            {
                _stats.AddRange(stats);
            }
            else
            {
                var stat = new Stats();
                stat.UpdateInfo(TypeOfUpdate.UseApp);
                _stats.Add(stat);
            }
        }

        internal void UpdateInfo(TypeOfUpdate type)
        {
            if (type == TypeOfUpdate.StandInQueue)
            {
                _stats.Add(new Stats());
            }
            _stats.Last().UpdateInfo(type);
        }

        public Stats CurrentState
        {
            get
            {                
                return _stats[_stats.Count - 1];
            }
        }

        public IList<Stats> Stats
        {
            get
            {
                return _stats;
            }
        }

    }
}