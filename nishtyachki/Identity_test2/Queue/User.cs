using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
namespace AdminApp.Queue
{
    public enum Role
    {
        premium,
        standart
    }
    public enum UserState
    {
        Offline,InQueue,WaitingForAccept,UsingNishtiak
    }
    public class User
    {
        public User(string id, Action tellToUse, Action<int> tellPossition)
        {
            this.ID = id;
            this.TellToUse = tellToUse;
            this.TellPossition = tellPossition;
            this.State = UserState.Offline;
          

        }
        public Action TellToUse { get; private set; }
        public Action<int> TellPossition   { get; private set; }
        public string ID { get; private set; }
        public string UserName { get; set; }
        public Stats Statistic { get; set; }
        public Role Role { get; set; }
        public UserState State { get; set; }
        public DateTime WasNoticedAboutNishtiak { get; set; }
        public Thread ThreadForCheckAnswerTime { get; set; }
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
            while(DateTime.Now.Subtract(WasNoticedAboutNishtiak)<UsersQueue.TimeForAccept)
            {

            }
            UsersQueue.DeleteUser(this);
         }
    }
}