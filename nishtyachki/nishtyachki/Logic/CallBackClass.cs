using nishtyachki.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace nishtyachki.Logic
{
    public class CallBackClass : AdminApp.IWcfServiceCallback
    {
        private IClientWindow _window;

        public CallBackClass(IClientWindow window)
        {
            _window = window;
        }

        public void NotifyServerReady()
        {
            Application.Current.Dispatcher.Invoke(() => _window.NotifyServerReady());
        }

        public void ShowMessage(string text)
        {
            Application.Current.Dispatcher.Invoke(() =>_window.ShowMessage(text));
        }

        public void NotifyToUseObj(string text)
        {
            Application.Current.Dispatcher.Invoke(() =>_window.NotifyToUseObj());
        }
    }
}
