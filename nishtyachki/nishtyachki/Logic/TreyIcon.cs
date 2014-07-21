using nishtyachki.Logic.Infrastructure;
using nishtyachki.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nishtyachki.Views;

namespace nishtyachki.Logic
{
    public class TreyIcon
    {
        private TreyNotifyWindow _tnw;

        public event Action HideWindow;
        public event Action ShowWindow;

        public bool IsVicible
        {
            get
            {
                return this._icon.Visible;
            }
            set
            {
                if (this._icon.Visible != value)
                {
                    if (value)
                    {
                        OnHideWindow();
                    }
                    else
                    {
                        OnShowWindow();
                    }
                }

                this._icon.Visible = value;
                if (value)
                {
                    this._tnw = new TreyNotifyWindow();
                    this._tnw.ShowMessage(AllStrings.HideWinMsg);
                    this._window.HideWindow();
                }
                else
                {
                    this._window.ShowWindow();
                    if (_tnw != null)
                        this._tnw.Close();
                }
            }
        }

        private NotifyIcon _icon;
        private IHideable _window;

        public TreyIcon(IHideable window)
        {
            this._icon = new NotifyIcon();
            this._icon.Icon = new System.Drawing.Icon(AllStrings.MainIco);

            this._window = window;

            IsVicible = false;

            _icon.MouseDoubleClick += _icon_MouseDoubleClick;
        }

        private void OnHideWindow()
        {
            if (HideWindow != null)
            {
                HideWindow();
            }
        }

        private void OnShowWindow()
        {
            if (ShowWindow != null)
            {
                ShowWindow();
            }
        }

        private void _icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IsVicible = false;
        }


    }
}
