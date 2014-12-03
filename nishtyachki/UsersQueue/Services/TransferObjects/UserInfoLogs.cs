using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace UsersQueue.Services.TransferObjects
{
    [Table("userinfo_logs")]
    public class UserInfoLogs
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        public int State { get; set; }
        [DataMember]
        public int Role { get; set; }
        [DataMember]
        public DateTime PremiumEndDate { get; set; }

    }
}
