using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace UsersQueue.Queue.UserInformtion
{
    [Table("UserInfo")]
    [DataContract]
    public class UserInfo
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public UserCurrentState State { get; set; }
        [DataMember]
        public DateTime? PremiumEndDate { get; set; }

        public Role Role
        {
            get
            {
                var result = Role.standart;

                if (PremiumEndDate != null)
                {
                    result = (PremiumEndDate > DateTime.Now) ? Role.premium : Role.standart; 
                }

                return result;
            }
        }

    }
}
