using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.App_Code.Interfaces
{
    interface IChangeUserRole
    {
        void ChangeRole(int ID, Role role);
    }
}
