using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UsersQueue.Queue.Nishtiachki;

namespace UsersQueue.Services.TransferObjects
{
    [DataContract(Name = "Nishtiachok")]
    public class NishtiakTransferObject
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public QueueUserTransferObject owner { get; set; }
        [DataMember]
        public int State { get; set; }
    }
}
