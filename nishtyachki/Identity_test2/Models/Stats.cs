using AdminApp.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Models
{
    public class Stats
    {
        public DateTime TimeOfBeginToStayInQuee { get; set; }
        public TimeSpan TimeOfStayingInQuee { get; set; }
        public DateTime TimeOfBeginToUseResource { get; set; }
        public TimeSpan TimeOfResourceUsing { get; set; }

        public Stats()
        {
          
        }
      internal  void UpdateInfo(TypeOfUpdate type)
        {
            switch(type)
            {
                case TypeOfUpdate.StandInQueue:
                    TimeOfBeginToStayInQuee = DateTime.Now;
                    break;
                case TypeOfUpdate.LeftQueueBeforeUsedNishtyak:
                    TimeOfStayingInQuee = DateTime.Now.Subtract(TimeOfBeginToStayInQuee);
                    break;
                case TypeOfUpdate.BeganToUseNishtyak:
                    TimeOfStayingInQuee = DateTime.Now.Subtract(TimeOfBeginToStayInQuee);
                    TimeOfBeginToUseResource = DateTime.Now;
                    break;
                case TypeOfUpdate.EndedToUseNishtyak:
                    TimeOfResourceUsing = DateTime.Now.Subtract(TimeOfBeginToUseResource);
                    break;
            }

        }
       

    
    }
}