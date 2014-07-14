using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.App_Code
{
    public enum Role
    {
        Premium,
        Standart
    }
    public class User
    {
        
        public string UserName { get; set; }
        public Statistics Stat { get; set; }
        public Role UserRole { get; set; }       
    }
}