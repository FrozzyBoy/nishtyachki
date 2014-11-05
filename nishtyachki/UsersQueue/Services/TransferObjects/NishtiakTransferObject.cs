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
        [DataMember]
        [MaxLength(100)]
        public string ID { get; set; }
        [DataMember]
        public virtual ICollection<NishtiakLogs> AllChanges { get; set; }
        #endregion EntityFrameWorkRegion

        [DataMember]        
        public QueueUserTransferObject owner { get; set; }
        [DataMember]
        public int State { get; set; }

        public bool IsActive { get; set; }

        public NishtiakTransferObject()
        {
            this.AllChanges = new List<NishtiakLogs>();
            this.IsActive = true;
        }
    }
}
