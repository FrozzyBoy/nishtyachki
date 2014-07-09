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

namespace nishtyachki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();
            icon.Icon = new System.Drawing.Icon(AllStrings.MainIco);

            icon.Visible = true;

            icon.DoubleClick += (sender, args) =>
            {
                this.Show();
                this.WindowState = System.Windows.WindowState.Normal;
            };
        }

        private void HideWindow()
        {
            this.Hide();
            System.Windows.Forms.MessageBox.Show(AllStrings.HideWinMsg);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
