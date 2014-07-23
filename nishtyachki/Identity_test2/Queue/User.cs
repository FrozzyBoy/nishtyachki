using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Timers;
using AdminApp.Services;
namespace AdminApp.Queue
{
    public enum Role
    {
        premium,
        standart
    }
    public enum UserState
    {
        Offline, InQueue, WaitingForAccept, UsingNishtiak
    }
    public class User
    {
        public User(string id,IClient IClient )
        {
            this.ID = id;
            //this.TellToUse = tellToUse;
            //this.TellPossition = tellPossition;
            this.State = UserState.Offline;
            this.Role = Queue.Role.standart;
            this.iClient = IClient;
            
        }
        public IClient iClient { get; set; }
        public Action TellToUse { get; private set; }
        public Action<int> TellPossition { get; private set; }
        public string ID { get; private set; }
        public string UserName { get; set; }
        public Stats Statistic { get; set; }
        public Role Role { get; set; }
        public UserState State { get; set; }      
        public Thread ThreadForCheckAnswerTime { get; set; }
        public Thread ThreadForCheckUsingTime { get; set; }
        private System.Timers.Timer _t;
        public override bool Equals(Object obj)
        {
            User user = obj as User;
            if (this.ID == user.ID)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        

        internal void CheckTimeForAcess()
        {
            _t = new System.Timers.Timer(120000);
            _t.Elapsed += t_Elapsed;
            _t.Start();
            
        }
        internal void CheckTimeForUsing()
        {


            _t = new System.Timers.Timer(6000000);
            _t.Elapsed += t_Elapsed;
            _t.Start();

        }    
        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UsersQueue.Instance.DeleteFromTheQueue(this);
            _t.Stop();
        }

    }
}