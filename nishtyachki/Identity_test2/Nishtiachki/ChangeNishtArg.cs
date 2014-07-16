using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminApp.Nishtiachki
{
    public enum TypeOfChanges
    {
        add,delete,change
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
    }
}