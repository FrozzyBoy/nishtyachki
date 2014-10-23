using System.Runtime.Serialization;

namespace UsersQueue.Services.TransferObjects
{
    [DataContract]
    public struct ChartValues
    {
        [DataMember]
        public string[] labels { get; set; }
        [DataMember]
        public int[] numbers { get; set; }
    }
}
