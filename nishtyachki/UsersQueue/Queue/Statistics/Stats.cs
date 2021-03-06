﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Queue.Statistics
{
    public class Stats
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserStateID")]
        public int UserInfoID;

        [MaxLength(100)]
        public string UserName { get; set; }

        public UserCurrentState NewState { get; set; }//state changed to
        public UserCurrentState OldState { get; set; }//state changed from
        public DateTime WhenHappend { get; set; }

        internal void UpdateInfo(UserCurrentState userState, UserCurrentState olduserState)
        {
            this.WhenHappend = DateTime.Now;
            this.NewState = userState;
            this.OldState = olduserState;
        }

    }
}
