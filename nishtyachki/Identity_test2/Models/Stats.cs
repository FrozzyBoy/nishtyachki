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
                case TypeOfUpdate.standInQueue:
                    TimeOfBeginToStayInQuee = DateTime.Now;
                    break;
                case TypeOfUpdate.leftQueueBeforeUsedNishtyak:
                    TimeOfStayingInQuee = DateTime.Now.Subtract(TimeOfBeginToStayInQuee);
                    break;
                case TypeOfUpdate.beganToUseNishtyak:
                    TimeOfStayingInQuee = DateTime.Now.Subtract(TimeOfBeginToStayInQuee);
                    TimeOfBeginToUseResource = DateTime.Now;
                    break;
                case TypeOfUpdate.endedToUseNishtyak:
                    TimeOfResourceUsing = DateTime.Now.Subtract(TimeOfBeginToUseResource);
                    break;
            }

        }
       

    
    }
}