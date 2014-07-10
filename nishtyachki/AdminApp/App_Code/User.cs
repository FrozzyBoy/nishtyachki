using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.App_Code
{
    public enum Role
    {
        standart,premium
    }
    public class User
    {
        public int ID { get;private set; }
        public Role Role { get; private set; }

    }
}