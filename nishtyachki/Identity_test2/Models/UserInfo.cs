using System;
using System.Collections.Generic;
using AdminApp.Queue;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminApp.Models
{
    public class UserInfo
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public UserState State;
        public DateTime PremiumEndDate;
        public List<Stats> Stats;

        public Role Role
        {
            get
            {
                if (PremiumEndDate > DateTime.Now)
                {
                    return Queue.Role.premium;
                }
                return Queue.Role.standart;
            }
        }

    }
}