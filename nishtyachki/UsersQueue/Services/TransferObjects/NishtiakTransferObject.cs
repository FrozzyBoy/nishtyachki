using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UsersQueue.Queue.Nishtiachki;

namespace UsersQueue.Services.TransferObjects
{
    [DataContract(Name = "Nishtiachok")]
    [Table("Nishtiachok")]
    public class NishtiakTransferObject
    {
        #region EntityFrameWorkRegion
        [Key]
        public int Key { get; set; }
        [MaxLength(100)]
        public string ChangeWas { get; set; }
        [ForeignKey("owner")]
        public int OwnerId { get; set; }
        #endregion EntityFrameWorkRegion

        [DataMember]
        [MaxLength(100)]
        public string ID { get; set; }
        [DataMember]        
        public QueueUserTransferObject owner { get; set; }
        [DataMember]
        public int State { get; set; }
    }
}
