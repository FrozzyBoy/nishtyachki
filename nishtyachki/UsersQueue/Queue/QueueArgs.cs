using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersQueue.Queue.Nishtiachki;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Queue
{
    public class QueueArgs : EventArgs
    {
        private TypeOfChanges _typeOfChange;
        private Role _role;

        public QueueArgs(TypeOfChanges typeOfChange, Role role)
        {
            _typeOfChange = typeOfChange;
            _role = role;
        }
        public QueueArgs(TypeOfChanges typeOfChange)
        {
            _typeOfChange = typeOfChange;
        }

        public Role GetRole
        {
            get
            {
                return _role;
            }
        }

    }
}
