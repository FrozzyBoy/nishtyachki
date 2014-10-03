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
        public string ID { get; set; }
        [DataMember]
        public int State { get; set; }
        [DataMember]
        public int Role { get; set; }
        [DataMember]
        public DateTime PremiumEndDate { get; set; }
    }
}
