using nishtyachki.Logic;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using nishtyachki.Resources;

namespace nishtyachki.Views
{
    /// <summary>
    /// Interaction logic for TreyNotifyWindow.xaml
    /// </summary>
    public partial class TreyNotifyWindow : Window
    {
        DoubleAnimation anim;
        int left;
        int top;
        DependencyProperty prop;
        int end;

        public TreyNotifyWindow()
        {
            InitializeComponent();

            TrayPos tpos = new TrayPos();
            tpos.getXY((int)this.Width, (int)this.Height, out top, out left, out prop, out end);
            this.Top = top;
            this.Left = left;

            var animationDuration = TimeSpan.FromSeconds(1);
            anim = new DoubleAnimation(end, animationDuration);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AnimationClock clock = anim.CreateClock();
            this.ApplyAnimationClock(prop, clock);
        }
        
        internal void ShowMessage(string msg)
        {
            lblMsg.Content = AllStrings.HideWinMsg;
            this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
