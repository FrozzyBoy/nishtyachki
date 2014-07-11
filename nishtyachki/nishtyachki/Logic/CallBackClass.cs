using nishtyachki.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nishtyachki.Logic
{
    public class CallBackClass : AdminApp.IWcfServiceCallback
    {
        IShowMessage _showMsg;

        public CallBackClass(IShowMessage showMsg)
        {
            _showMsg = showMsg;
        }

        public void ShowMessage(string text)
        {
            _showMsg.ShowMessage(text);
        }


        public void NotifyToUseObj(string text)
        {
            _showMsg.ShowMessage(text);
        }
    }
}
