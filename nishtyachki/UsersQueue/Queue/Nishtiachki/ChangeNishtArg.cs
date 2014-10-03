using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersQueue.Queue.Nishtiachki
{
    public enum TypeOfChanges
    {
        add, delete, change,
        blocked,
        opened,
        create
    }
    public class ChangeNishtArg : EventArgs
    {
        TypeOfChanges _typeOfChange;
        Nishtiachok_State _state;

        public ChangeNishtArg(TypeOfChanges typeOfChange, Nishtiachok_State state)
        {
            _typeOfChange = typeOfChange;
            _state = state;
        }
        public ChangeNishtArg(TypeOfChanges typeOfChange)
        {
            _typeOfChange = typeOfChange;
        }

        public Nishtiachok_State State
        {
            get
            {
                return _state;
            }
        }

        public TypeOfChanges TypeOfChange
        {
            get
            {
                return _typeOfChange;
            }
        }
    }
}
