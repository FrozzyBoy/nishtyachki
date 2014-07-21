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
        private INotifyWindow _notify;

        public CallBackClass(IClientWindow window, INotifyWindow notify)
        {
            _window = window;
            _notify = notify;
        }

        public void NotifyServerReady()
        {
            Application.Current.Dispatcher.Invoke(() => _window.NotifyServerReady());
        }

        public void ShowMessage(string text)
        {
            Application.Current.Dispatcher.Invoke(() =>_window.ShowMessage(text));
        }

        public void NotifyToUseObj()
        {
            Application.Current.Dispatcher.Invoke(() =>_window.NotifyToUseObj());
        }
        
        public void StandInQueue()
        {
            Application.Current.Dispatcher.Invoke(() => _window.StandInQueue());
        }

        public void ShowPosition(int position)
        {
            Application.Current.Dispatcher.Invoke(() => _notify.ShowPosition(position));
        }
        
        public bool SuggestToUseObject()
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                _notify.SuggestToUseObject(); 
            });

            return _notify.Result == NotifyResult.Ok;
        }
    }
}
