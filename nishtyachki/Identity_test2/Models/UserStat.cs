using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using AdminApp.Queue;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Models
{
    public class UserStats
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public List<Stats> _stats;

        [Obsolete("Only needed for serialization and materialization", true)]
        public UserStats()
        { }

        public UserStats(string userID)
        {
            this.UserName = userID;
            _stats = new List<Stats>();
            UpdateInfo(UserCurrentState.Online, UserCurrentState.Offline);
        }

        internal void UpdateInfo(UserCurrentState newState, UserCurrentState oldState)
        {
            var newStat = new Stats();
            newStat.UpdateInfo(newState, oldState);
            newStat.UserName = this.UserName;

            if (_stats == null)
            {
                _stats = new List<Stats>();
            }
            _stats.Add(newStat);

            using (var context = new AppDbContext())
            {
                context.SaveChanges();
            }

        }

        [NotMapped] 
        public Stats CurrentState
        {
            get
            {                
                return _stats[_stats.Count - 1];
            }
        }

        [NotMapped] 
        public IList<Stats> Stats
        {
            get
            {
                if(_stats == null)
                {
                    _stats = new List<Stats>();
                }
                return _stats;
            }
        }


        internal static UserStats GetUserStat(string userID)
        {
            UserStats stats = null;
            using (var context = new AppDbContext())
            {
                stats = context.UserStats.SingleOrDefault<UserStats>(x => x.UserName == userID);
                if (stats == null)
                {
                    stats = new UserStats(userID);
                    context.UserStats.Add(stats);
                    context.SaveChanges();
                }
            }

            return stats;                
        }
    }
}