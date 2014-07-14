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

        public MainWindow()
        {
            InitializeComponent();
            _treyIcon = new TreyIcon(this);
            this.EnqueueEnter += MainWindow_EnqueueEnter_HideWindow;

            this.btnEnqueue.Content = AllStrings.BtnTextInit;
            btnEnqueue.IsEnabled = false;

            _repo = new Repository(this);
        }

        private void MainWindow_EnqueueEnter_HideWindow()
        {
            _treyIcon.IsVicible = true;
            _repo.StayInQueue();
        }

        public void HideWindow()
        {
            this.Hide();
            ShowMessage(AllStrings.HideWinMsg);
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
            ShowMessage("use it!");
        }

    }
}
