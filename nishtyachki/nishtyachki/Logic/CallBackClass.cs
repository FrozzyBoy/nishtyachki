using nishtyachki.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

        async public void NotifyServerReady()
        {
            Application.Current.Dispatcher.Invoke(() => _window.NotifyServerReady());
        }

        async public void ShowMessage(string text)
        {
            Application.Current.Dispatcher.Invoke(() => _window.ShowMessage(text));
        }

        async public void ShowPosition(int position)
        {
            Application.Current.Dispatcher.Invoke(() => _notify.ShowPosition(position));
        }

        async public void OfferToUseObj()
        {
            Application.Current.Dispatcher.Invoke(() => _window.OfferToUseObj());
        }

        async public void NotifyToUseObj()
        {
            Application.Current.Dispatcher.Invoke(() => _window.NotifyToUseObj());
        }

        async public void StandInQueue()
        {
            Application.Current.Dispatcher.Invoke(() => _window.StandInQueue());
        }

        async public void DroppedByServer(string text)
        {
            Application.Current.Dispatcher.Invoke(() => _window.DroppedByServer(text));
        }
    }
}
