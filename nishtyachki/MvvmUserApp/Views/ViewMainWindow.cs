using MvvmUserApp.Resources;
using MvvmUserApp.ViewModel.TreyView;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace MvvmUserApp.Views
{
    public class ViewMainWindow : IStateChangerWindow, IDisposable
    {
        private System.Windows.Window _view;
        private NotifyIcon _icon;
        WindowFromTrey _windowFromTrey;

        public ViewMainWindow(Window view)
        {
            _view = view;
            _icon = new NotifyIcon();
            _icon.DoubleClick += _icon_DoubleClick;
            _icon.Icon = ResourceIcons.Main;
        }

        void _icon_DoubleClick(object sender, System.EventArgs e)
        {
            _view.Show();
            if (_windowFromTrey != null)
            {
                _windowFromTrey.Close();
            }
            _icon.Visible = false;
        }

        public void Close()
        {
            _view.Close();
        }

        public void Hide()
        {
            _view.Hide();

            _windowFromTrey = new WindowFromTrey();
            _windowFromTrey.Show();

            _icon.Visible = true;
        }

        ~ViewMainWindow()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_icon != null)
                {
                    _icon.Dispose();
                }
            }

            _view = null;
            _icon = null;
        }
    }
}
