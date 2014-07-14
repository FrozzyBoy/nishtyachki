﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using nishtyachki.Resources;
using nishtyachki.Logic.Infrastructure;
using nishtyachki.Logic;
using System.ServiceModel;

namespace nishtyachki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IShowMessage, IHideable
    {
        public event Action EnqueueEnter;
        public event Action EnqueueError;

        private IRepository _repo;
        private TreyIcon _treyIcon;

        public MainWindow()
        {
            _repo = new TempRepo();
            InitializeComponent();
            _treyIcon = new TreyIcon(this);
            this.EnqueueEnter += MainWindow_EnqueueEnter_HideWindow;
        }

        private void MainWindow_EnqueueEnter_HideWindow()
        {
            _treyIcon.IsVicible = true;

            ShowMessageToUser(string.Format(AllStrings.ShowNumberOfPeople, _repo.NumberOfPeopleInFrontOfMe));

            AdminApp.IWcfServiceCallback callBack = new CallBackClass(this);
            InstanceContext ic = new InstanceContext(callBack);
            AdminApp.IWcfService service = new AdminApp.WcfServiceClient(ic);
            service.StandInQueue();            
        }

        public void HideWindow()
        {
            this.Hide();
            ShowMessageToUser(AllStrings.HideWinMsg);
        }

        private void ShowMessageToUser(string message)
        {
            System.Windows.Forms.MessageBox.Show(message); 
        }
                
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                _treyIcon.IsVicible = false;
            }
            else
            {
                _treyIcon.IsVicible = true;
            }

            base.OnStateChanged(e);
        }

        private void Enqueue_Click(object sender, RoutedEventArgs e)
        {            
            bool isOkRequest = _repo.SendRequest();

            if (isOkRequest)
            {
                OnEnqueueEnter();
            }
            else
            {
                OnEnqueueError();
            }
        }

        public void OnEnqueueEnter()
        {
            if (EnqueueEnter != null)
            {
                EnqueueEnter();
            } 
        }

        public void OnEnqueueError()
        {
            if (EnqueueError != null)
            {
                EnqueueError();
            }
        }

        public void ShowMessage(string msg)
        {
            ShowMessageToUser(msg);
        }

        public void ShowWindow()
        {
            this.Show();
            this.WindowState = System.Windows.WindowState.Normal;
        }
    }
}
