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
    public partial class MainWindow : Window
    {
        private IRepository _repo;

        public event Action EnqueueEnter;
        public event Action EnqueueError;

        public MainWindow()
        {
            _repo = new TempRepo();

            InitializeComponent();

            System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();
            icon.Icon = new System.Drawing.Icon(AllStrings.MainIco);

            icon.Visible = true;

            icon.DoubleClick += (sender, args) =>
            {
                this.Show();
                this.WindowState = System.Windows.WindowState.Normal;
            };

            this.EnqueueEnter += MainWindow_EnqueueEnter_HideWindow;
            this.EnqueueEnter += SayHello;
        }

        private void SayHello()
        {
            var myServiceHost = new ServiceHost(typeof(MyService));
            myServiceHost.Open();

            /*
            WCFTestService.MyServiceClient myService =
         new WCFTestService.MyServiceClient();
            MessageBox.Show(myService.DoWork("Hello World!"));
            myService.Close();
            */

            this.btnStopConnect.Click += (sender, e) =>
                {
                    if (myServiceHost != null)
                    {
                        myServiceHost.Close();
                        myServiceHost = null;
                    }
                };

        }

        private void MainWindow_EnqueueEnter_HideWindow()
        {
            this.HideWindow();
            ShowMessageToUser(string.Format(AllStrings.ShowNumberOfPeople, _repo.NumberOfPeopleInFrontOfMe));
        }

        private void HideWindow()
        {
            this.Hide();
            ShowMessageToUser(AllStrings.HideWinMsg);
        }

        private void ShowMessageToUser(string message)
        {
            System.Windows.Forms.MessageBox.Show(message); 
        }

        private void btnHideWindow_Click(object sender, RoutedEventArgs e)
        {
            HideWindow();
        }
        
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                HideWindow(); 
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

    }
}
