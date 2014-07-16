using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Queue
{
    public enum Role
    {
        premium,
        standart
    }
    public class User
    {
        public User(string id, Action tellToUse, Action<int> tellPossition)
        {
            this.ID = id;
            this.TellToUse = tellToUse;
            this.TellPossition = tellPossition;
        }
        public Action TellToUse { get; private set; }
        public Action<int> TellPossition   { get; private set; }
        public string ID { get; private set; }
        public string UserName { get; set; }
        public Statistics Statistic { get; set; }
        public Role Role { get; set; }
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
            return base.GetHashCode();
        }
    }
}