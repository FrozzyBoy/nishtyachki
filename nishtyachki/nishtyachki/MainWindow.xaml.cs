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

        private enum CurrentObjUseStatus
        {
            UnUse,
            Offered,            
            InQueue,
            Use
        }

        private IRepository _repo;
        private TreyIcon _treyIcon;

        private NotifyWindow _notifyToUse;

        private CurrentObjUseStatus _useStatus = CurrentObjUseStatus.UnUse;

        public MainWindow()
        {
            InitializeComponent();
                        
            _treyIcon = new TreyIcon(this);

            this.btnEnqueue.Content = AllStrings.BtnTextInit;
            btnEnqueue.IsEnabled = false;
            
            _notifyToUse = new NotifyWindow(this);
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
            _useStatus = CurrentObjUseStatus.UnUse;
            this.btnEnqueue.Content = AllStrings.BtnTextReady;
            btnEnqueue.IsEnabled = true;
        }

        public void NotifyToUseObj()
        {
            _useStatus = CurrentObjUseStatus.Use;            
        }

        public void StandInQueue()
        {
            _useStatus = CurrentObjUseStatus.InQueue;
            string msg = AllStrings.MsgUserInQueue;
            ShowMessage(msg);
            _treyIcon.IsVicible = true;
            btnEnqueue.IsEnabled = false;
        }

        public void OfferToUseObj()
        {
            _useStatus = CurrentObjUseStatus.Offered;
            _notifyToUse.Show();
            _notifyToUse.OfferToUseObj();

        }

        public void AnswerForOffer(bool willUse)
        {
            switch (_useStatus)
            {
                case CurrentObjUseStatus.UnUse:
                    break;
                case CurrentObjUseStatus.Offered:
                    _repo.AnswerForOfferToUse(willUse);
                    break;
                case CurrentObjUseStatus.InQueue:
                    _repo.LeaveQueue();
                    break;
                case CurrentObjUseStatus.Use:
                    break;
                default:
                    break;
            }
        }
    }
}
