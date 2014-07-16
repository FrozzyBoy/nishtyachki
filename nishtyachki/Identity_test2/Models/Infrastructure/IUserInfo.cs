using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Models.Infrastructure
{
    public interface IUserInfo
    {
        void UsersUseTimeCount(out DateTime[] time, out int[] count);
    }
}
