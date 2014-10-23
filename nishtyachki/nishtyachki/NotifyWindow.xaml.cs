using nishtyachki.Logic.Infrastructure;
using nishtyachki.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Timers;
using nishtyachki.Logic;

namespace nishtyachki
{
    /// <summary>
    /// Interaction logic for NotifyWindow.xaml
    /// </summary>
    public partial class NotifyWindow : Window, INotifyWindow
    {
        private IClientWindow _window;
        private const int MINUTE = 60000;
        private const int AFK_TIME = 10 * MINUTE;
        private const int CHEK_AFK_TIME = 1 * MINUTE;
        private Timer _timer;

        public NotifyWindow(IClientWindow window)        
        {   
            InitializeComponent();

            this.lblNotification.Content = "Wait.";

            this.btnOk.IsEnabled = false;

            _window = window;

            _timer = new Timer(CHEK_AFK_TIME);

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
            _timer.Elapsed -= StandInQueueElapsed;
            _timer.Stop();

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
            _timer.Elapsed += StandInQueueElapsed;
            _timer.Start();

            this.lblNotification.Content = AllStrings.MsgUserInQueue;
            this.btnOk.IsEnabled = false;

        }

        private void StopTimer()
        {
            _timer.Elapsed -= StandInQueueElapsed;
            _timer.Stop(); 
        }

        private void StandInQueueElapsed(object sender, ElapsedEventArgs e)
        {
            if (Win32.GetIdleTime() > AFK_TIME)
            {
                StopTimer();

                Dispatcher.Invoke(() =>
                {
                    _window.ShowMessage("kick for afk");
                    btn_ClickLeaveQueue(this, new RoutedEventArgs());
                });
            }
        }

        #endregion indata

        #region outdata
        private void btn_ClickStopUse(object sender, RoutedEventArgs e)
        {
            StopTimer();
            _window.StopUse();
            this.Hide();
        }

        void btn_ClickLeaveQueue(object sender, RoutedEventArgs e)
        {
            StopTimer();
            _window.LeaveQueue();
            this.Hide();
        }
        
        void btn_ClickOffered(object sender, RoutedEventArgs e)
        {
            StopTimer();
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
            StopTimer();
            this.lblNotification.Content = "Wait.";
            this.btnOk.IsEnabled = false;
            this.Hide();
        }
    }
}