using MvvmUserApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvvmUserApp.ViewModel
{
    public class ShowMessages : IShowMessages
    {
        public void Show(string text)
        {
            new Thread(() =>
            {
                MessageBox.Show(text, ResourceStrings.MessageBoxHead);
            }).Start();
        }
    }
}
