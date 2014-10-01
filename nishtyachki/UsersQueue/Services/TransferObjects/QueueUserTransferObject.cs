using System;
using System.Runtime.Serialization;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Services.TransferObjects
{
    
    [DataContract(Name = "QueueUser")]
    public class QueueUserTransferObject
    {
        [DataMember]
        public string ID;
        [DataMember]
        public int State;
        [DataMember]
        public int Role;
        [DataMember]
        public DateTime PremiumEndDate;
    }
}
