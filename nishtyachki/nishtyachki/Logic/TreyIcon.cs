﻿using nishtyachki.Logic.Infrastructure;
using nishtyachki.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nishtyachki.Logic
{
    public class TreyIcon
    {        
        public bool IsVicible
        {
            get
            {
                return this._icon.Visible;
            }
            set
            {
                this._icon.Visible = value;
                if (value)
                {
                    this._window.HideWindow();
                }
                else
                {
                    this._window.ShowWindow();
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

        void _icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            IsVicible = false;
        }

    }
}
