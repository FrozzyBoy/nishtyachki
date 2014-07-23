﻿using System;
using System.Windows;
using nishtyachki.Resources;
using nishtyachki.Logic.Infrastructure;
using nishtyachki.Logic;
using System.Threading;


namespace nishtyachki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClientWindow, IHideable
    {
        private IRepository _repo;
        private TreyIcon _treyIcon;

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

            Restart();
        }

        private void Restart()
        {
            if (_notifyToUse != null)
            {
                _notifyToUse.Close();
            }

            this.btnEnqueue.Content = AllStrings.BtnTextInit;
            btnEnqueue.IsEnabled = false;

            _notifyToUse = new NotifyWindow(this);
            _notifyToUse.Hide();

            _repo = new Repository(this, _notifyToUse);

            _treyIcon = new TreyIcon(this);
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
            MessageBox.Show(msg);
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
                Restart();
            }
            _repo.AnswerForOfferToUse(willUse);
        }

        public void LeaveQueue()
        {
            _repo.LeaveQueue();
            Restart();
        }

        public void StopUse()
        {
            _repo.StopUse();
            Restart();
        }

        public void DroppedByServer(string text)
        {
            ShowMessage(text);
            Restart();
        }
    }
}
