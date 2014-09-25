using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersQueue.Queue.UserInformtion
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public UserCurrentState State { get; set; }
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
