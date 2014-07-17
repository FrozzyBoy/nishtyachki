using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Models
{
    public class UserStat
    {
       internal List<Stats> _stats;
        public UserStat()
        {
            _stats = new List<Stats>();           
        }
      internal  void UpdateInfo(TypeOfUpdate type)
        {
          if(type==TypeOfUpdate.standInQueue)
          {
              _stats.Add(new Stats());
          }
            _stats.Last().UpdateInfo(type);          
        }
    }
}