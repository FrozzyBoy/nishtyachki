﻿using nishtyachki.Logic.Infrastructure;
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
            Application.Current.Dispatcher.Invoke(() => _window.ShowMessage(text));
        }

        public void ShowPosition(int position)
        {
            Application.Current.Dispatcher.Invoke(() => _window.ShowPosition(position));
        }

        public void OfferToUseObj()
        {
            Application.Current.Dispatcher.Invoke(() => _window.OfferToUseObj());
        }

        public void NotifyToUseObj()
        {
            Application.Current.Dispatcher.Invoke(() => _window.NotifyToUseObj());
        }

        public void StandInQueue()
        {
            Application.Current.Dispatcher.Invoke(() => _window.StandInQueue());
        }

        public void DroppedByServer(string text)
        {
            Application.Current.Dispatcher.Invoke(() => _window.DroppedByServer(text));
        }
    }
}
