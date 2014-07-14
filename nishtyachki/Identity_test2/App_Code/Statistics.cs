using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.App_Code
{
    public class Statistics
    {
        public DateTime TimeOfUsing { get; set; }
        public DateTime SettingTime { get; set; }
        public int Position { get; set; }
        public TimeSpan TimeOfResourceUsing { get; set; }

        public override string ToString()
        {
            return "Write me";
        }
    }
}