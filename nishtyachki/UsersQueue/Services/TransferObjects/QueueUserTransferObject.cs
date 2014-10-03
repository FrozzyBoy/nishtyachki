using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Services.TransferObjects
{
    
    [DataContract(Name = "QueueUser")]
    [Table("QueueUser")]
    public class QueueUserTransferObject
    {
        [Key]
        public int Key { get; set; }                
        [DataMember]
        [MaxLength(100)]
        public string ID;
        [DataMember]
        public int State;
        [DataMember]
        public int Role;
        [DataMember]
        public DateTime PremiumEndDate;
    }
}
