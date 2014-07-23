using nishtyachki.Logic.Infrastructure;
using nishtyachki.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace nishtyachki
{
    /// <summary>
    /// Interaction logic for NotifyWindow.xaml
    /// </summary>
    public partial class NotifyWindow : Window, INotifyWindow
    {
        IClientWindow _window;
        public NotifyWindow(IClientWindow window)
        {
            InitializeComponent();

            this.lblNotification.Content = "Wait.";

            this.btnOk.IsEnabled = false;

            _window = window;

            this.btnOk.Click += btn_ClickOffered;
            this.btnCancel.Click += btn_ClickInQueue;
        }

        #region indata
        public void NotifyToUseObj()
        {
            lblNotification.Content = AllStrings.MsgUserUseObj;

            this.btnCancel.Click -= btn_ClickInQueue;
            this.btnCancel.Click += btn_ClickOffered;
        }

        public void OfferToUseObj()
        {
            this.btnOk.IsEnabled = true;
            lblNotification.Content = AllStrings.MsgUserOfferedToUse;

            this.btnCancel.Click -= btn_ClickOffered;
            this.btnCancel.Click += btn_ClickStopUse;
        }

        public void ShowPosition(int pos)
        {
            string msg = string.Format(
                AllStrings.NotifyUserPosition,
                pos);

            lblNotification.Content = msg;
        }
        #endregion indata

        #region outdata
        private void btn_ClickStopUse(object sender, RoutedEventArgs e)
        {
            _window.StopUse();
            this.Hide();
        }

        void btn_ClickInQueue(object sender, RoutedEventArgs e)
        {
            _window.LeaveQueue();
            this.Hide();
        }
        
        void btn_ClickOffered(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            bool ok = but.Content == btnOk.Content;

            if (ok)
            {
                btnOk.IsEnabled = false;
            }

            _window.AnswerForOffer(ok);            
        }
        #endregion outdata
    }
}