using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace MvvmUserApp.ViewModel.TreyView
{
    /// <summary>
    /// Interaction logic for WindowFromTrey.xaml
    /// </summary>
    public partial class WindowFromTrey : Window
    {
        const int ANIMATION_DURATION_IN_SECONDS = 1;

        public WindowFromTrey()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation anim;
            int left;
            int top;
            DependencyProperty prop;
            int end;

            TrayPos tpos = new TrayPos();
            tpos.getXY((int)this.Width, (int)this.Height, out top, out left, out prop, out end);
            this.Top = top;
            this.Left = left;

            var animationDuration = TimeSpan.FromSeconds(ANIMATION_DURATION_IN_SECONDS);
            anim = new DoubleAnimation(end, animationDuration);

            AnimationClock clock = anim.CreateClock();
            this.ApplyAnimationClock(prop, clock);
        }

    }
}
