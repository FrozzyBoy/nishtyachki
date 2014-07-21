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
        public NotifyResult Result
        {
            get;
            private set;
        }

        public NotifyWindow()
        {
            InitializeComponent();            
            
            this.Result = NotifyResult.Nothing;
            this.lblNotification.Content = "Wait.";

            this.btnOk.IsEnabled = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Result = NotifyResult.Ok;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = NotifyResult.Cancel;
            this.Hide();
        }

        public void ShowPosition(int pos)
        {
             string msg = string.Format(
                 AllStrings.NotifyUserPosition,
                 pos);

             lblNotification.Content = msg;
        }

        public void SuggestToUseObject()
        {
            this.btnOk.IsEnabled = true;
            lblNotification.Content = AllStrings.MsgUserUseObj;
        }

    }
}
