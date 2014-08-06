using AdminApp.Queue;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Models
{
    public class Stats
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserInfoID")]
        public int UserInfoID { get; set; }

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