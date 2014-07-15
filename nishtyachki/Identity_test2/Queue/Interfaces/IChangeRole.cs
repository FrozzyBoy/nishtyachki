using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Queue.Interfaces
{
    interface IChangeRole
    {
        void ChangeRole(int id, Role role);
    }
}
