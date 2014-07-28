using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Nishtiachki;
namespace AdminApp.Queue
{
    public class QueueArgs : EventArgs
    {

        TypeOfChanges _typeOfChange;
        Role _role;
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