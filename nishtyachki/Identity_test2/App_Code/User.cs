using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.App_Code
{
    public enum Role
    {
        premium,
        standart
    }
    public class User
    {
        public User(string id)
        {
            this.ID = id;
        }
        public string ID { get; private set; }
        public string UserName { get; set; }
        public Statistics Statistic { get; set; }
        public Role Role { get; set; }       
    }
}