using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace UsersQueue.Services.TransferObjects
{
    [Table("nishtiak_logs")]
    public class NishtiakLogs
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        public string NishtiakId { get; set; }
        [DataMember]
        public string ChangeWas { get; set; }
        [DataMember]
        public DateTime? ChangeHappend { get; set; }
        [DataMember]
        public int State { get; set; }
        [DataMember]        
        public virtual QueueUserTransferObject Owner { get; set; }

        public NishtiakLogs()
        {
            ChangeHappend = DateTime.Now;
        }
    }
}
