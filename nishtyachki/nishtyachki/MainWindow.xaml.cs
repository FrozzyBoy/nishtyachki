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
    public partial class MainWindow : Window, IClientWindow, IHideable
    {
        public event Action EnqueueEnter;
        public event Action EnqueueError;

        private IRepository _repo;
        private TreyIcon _treyIcon;

        private NotifyWindow _notifyToUse;

        public MainWindow()
        {
            InitializeComponent();
                        
            _treyIcon = new TreyIcon(this);
            this.EnqueueEnter += MainWindow_EnqueueEnter_HideWindow;
            this.EnqueueEnter += MainWindow_EnqueueEnter;

            this.btnEnqueue.Content = AllStrings.BtnTextInit;
            btnEnqueue.IsEnabled = false;
            
            _notifyToUse = new NotifyWindow();
            _notifyToUse.Hide();

            _repo = new Repository(this, _notifyToUse);
        }

        void MainWindow_EnqueueEnter()
        {
            _notifyToUse.Show();
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
            OnEnqueueEnter();
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
            btnEnqueue.IsEnabled = true;
            SwitchButtonStatus(AllStrings.MsgUserUseObj, true, AllStrings.BtnTextReady);
        }

        public void StandInQueue()
        {
            string msg = string.Format(AllStrings.MsgUserInQueue);
            SwitchButtonStatus(msg, false, AllStrings.BtnTextInqueue);
        }

        private void SwitchButtonStatus(string msg, bool isAnabled, string btnContent)
        {
            this.btnEnqueue.IsEnabled = isAnabled;
            this.btnEnqueue.Content = btnContent;
            ShowMessage(msg);
        }

    }
}
