using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nishtyachki.Logic.Infrastructure;

namespace nishtyachki.Logic
{
    public class TempRepo : IRepository
    {
        public bool SendRequest()
        {
            return true;
        }

        public int NumberOfPeopleInFrontOfMe
        {
            get
            {
                return 5;// прост 5 
            }
        }
    }
}
