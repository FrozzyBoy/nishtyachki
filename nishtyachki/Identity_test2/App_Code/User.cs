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
            _ID = id;
        }
        public string _ID { get; set; }
        public string _userName { get; set; }
        public Statistics _statistic { get; set; }
        public Role _role { get; set; }       
    }
}