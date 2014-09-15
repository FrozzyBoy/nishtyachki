using System;
using System.Windows;
using nishtyachki.Resources;
using nishtyachki.Logic.Infrastructure;
using nishtyachki.Logic;
using System.Threading;
using log4net;


namespace nishtyachki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClientWindow, IHideable
    {
        private IRepository _repo;
        private TreyIcon _treyIcon;

        private static readonly ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private NotifyWindow _notifyToUse;

        private static Mutex mutex;

        public MainWindow()
        {
            InitializeComponent();

            bool isUnique;
            mutex = new Mutex(true, "uniqe_app_mutex", out isUnique);
            if (!isUnique)
            {
                MessageBox.Show(AllStrings.AppIsRun);
                Environment.Exit(0);
            }
            GC.KeepAlive(mutex);

            _treyIcon = new TreyIcon(this);

            var currentApp = Application.Current;

            this.Closing += MainWindow_Closing;

            currentApp.DispatcherUnhandledException += currentApp_DispatcherUnhandledException;
            _notifyToUse = new NotifyWindow(this);
            _repo = new Repository(this);

            Restart(false);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void currentApp_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //throw new NotImplementedException();
            //e.Handled = true;
            //e.Exception;
            _log.Error("unhadled exception", e.Exception);
            ShowMessage(e.Exception.Message);
            e.Handled = true;
        }

        private void Restart(bool isRestart)
        {
            _treyIcon.IsVicible = false;
            
            this.btnEnqueue.Content = AllStrings.BtnTextInit;
            btnEnqueue.IsEnabled = false;

            _notifyToUse.Restart();

            if (isRestart)
            {
                this.NotifyServerReady();
            }

        }

        void MainWindow_EnqueueEnter()
        {
            _notifyToUse.StandInQueue();
        }

        private void MainWindow_EnqueueEnter_HideWindow()
        {
            _treyIcon.IsVicible = true;
            _repo.StayInQueue();
        }

        public void HideWindow()
        {
            this.Hide();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    break;
                case WindowState.Minimized:
                    _treyIcon.IsVicible = true;
                    break;
                case WindowState.Normal:
                    _treyIcon.IsVicible = false;
                    break;
                default:
                    break;
            }

            base.OnStateChanged(e);
        }

        private void Enqueue_Click(object sender, RoutedEventArgs e)
        {
            _repo.StayInQueue();
        }

        public void ShowMessage(string msg)
        {
            //this.Invoke();
            var thread = new Thread(() => MessageBox.Show(msg));
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.Lowest;
            thread.Start();
        }

        public void ShowWindow()
        {
            this.Show();
            this.WindowState = System.Windows.WindowState.Normal;
        }

        public void NotifyServerReady()
        {
            this.btnEnqueue.Content = AllStrings.BtnTextReady;
            btnEnqueue.IsEnabled = true;
        }

        public void NotifyToUseObj()
        {
            _notifyToUse.Show();
            _notifyToUse.NotifyToUseObj();
        }

        public void StandInQueue()
        {            
            _treyIcon.IsVicible = true;
            btnEnqueue.IsEnabled = false;
            string msg = AllStrings.MsgUserInQueue;
            ShowMessage(msg);
        }

        public void OfferToUseObj()
        {
            _notifyToUse.Show();
            _notifyToUse.OfferToUseObj();
        }

        public void AnswerForOffer(bool willUse)
        {
            if (!willUse)
            {
                _notifyToUse.Hide();
                _treyIcon.IsVicible = false;
                Restart(true);
            }
            _repo.AnswerForOfferToUse(willUse);
        }

        public void LeaveQueue()
        {
            _repo.LeaveQueue();
            Restart(true);
        }

        public void StopUse()
        {
            _repo.StopUse();
            Restart(true);
        }

        public void DroppedByServer(string text)
        {
            ShowMessage(text);
            Restart(true);
        }

        public void ShowPosition(int pos)
        {
            _notifyToUse.ShowPosition(pos);
        }
    }
}
