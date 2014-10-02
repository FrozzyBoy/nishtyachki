using System.Collections.Generic;
using System.Linq;
using UsersQueue.Model;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Queue.Statistics
{
    public class UserStats
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public List<Stats> _stats;

        public UserStats(string userID)
        {
            this.UserName = userID;
            _stats = new List<Stats>();
            UpdateInfo(UserCurrentState.Online, UserCurrentState.Offline);
        }

        internal void UpdateInfo(UserCurrentState newState, UserCurrentState oldState)
        {
            if (newState == oldState)
            {
                return;
            }

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
                context.Stats.Add(newStat);
                context.SaveChanges();
            }

        }

        public Stats CurrentState
        {
            get
            {
                var stats = StatsForUser;
                return stats[stats.Count - 1];
            }
        }

        public IList<Stats> StatsForUser
        {
            get
            {
                using (var context = new AppDbContext())
                {
                    var selected = from st in context.Stats
                                   where st.UserName == this.UserName
                                   select st;
                    _stats = selected.ToList<Stats>();
                }

                if (_stats == null)
                {
                    this.UpdateInfo(UserCurrentState.Online, UserCurrentState.Offline);
                }
                return _stats;
            }
        }

        public int CountWasInState(UserCurrentState state)
        {
            int count = 0;

            var userStats = StatsForUser;
            foreach (var stat in userStats)
            {
                count += ((stat.NewState == state) ? 1 : 0);
            }

            return count;
        }

        internal static UserStats GetUserStat(string userID)
        {
            UserStats stats = new UserStats(userID);
            return stats;
        }
    }
}
