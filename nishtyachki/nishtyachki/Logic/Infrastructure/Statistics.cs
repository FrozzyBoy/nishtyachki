using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Models
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