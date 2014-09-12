using nishtyachki.Logic.Infrastructure;
using nishtyachki.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace nishtyachki
{
    /// <summary>
    /// Interaction logic for NotifyWindow.xaml
    /// </summary>
    public partial class NotifyWindow : Window, INotifyWindow
    {
        private IClientWindow _window;

        public NotifyWindow(IClientWindow window)        
        {   
            InitializeComponent();

            this.lblNotification.Content = "Wait.";

            this.btnOk.IsEnabled = false;

            _window = window;

            this.Hide();
        }

        #region indata
        public void NotifyToUseObj()
        {
            lblNotification.Content = AllStrings.MsgUserUseObj;

            this.btnOk.IsEnabled = false;
            this.btnOk.Click -= btn_ClickOffered;

            this.btnCancel.Click -= btn_ClickOffered;
            this.btnCancel.Click += btn_ClickStopUse;
        }

        public void OfferToUseObj()
        {
            this.btnOk.IsEnabled = true;
            lblNotification.Content = AllStrings.MsgUserOfferedToUse;

            this.btnOk.IsEnabled = true;
            this.btnOk.Click += btn_ClickOffered;

            this.btnCancel.Click -= btn_ClickLeaveQueue;
            this.btnCancel.Click += btn_ClickOffered;
        }

        public void ShowPosition(int pos)
        {
            string msg = string.Format(
                AllStrings.NotifyUserPosition,
                pos);

            lblNotification.Content = msg;
        }

        public void StandInQueue()
        {
            this.Show();
            this.btnCancel.Click += btn_ClickLeaveQueue;
        }

        #endregion indata

        #region outdata
        private void btn_ClickStopUse(object sender, RoutedEventArgs e)
        {
            _window.StopUse();
            this.Hide();
        }

        void btn_ClickLeaveQueue(object sender, RoutedEventArgs e)
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

        internal void Restart()
        {
            this.lblNotification.Content = "Wait.";
            this.btnOk.IsEnabled = false;
            this.Hide();
        }
    }
}