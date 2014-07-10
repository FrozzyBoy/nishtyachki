using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace nishtyachki.Logic.Service
{
    [DataContract]
    public class Registration
    {
        [DataMember]
        public int UserId { get; private set; }
        
        public Registration(int userId)
        {
            this.UserId = userId;
        }

    }
}
