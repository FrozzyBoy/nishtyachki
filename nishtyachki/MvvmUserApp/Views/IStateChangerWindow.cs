using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUserApp.Views
{
    public interface IStateChangerWindow
    {
        void Close();
        void Hide();
    }
}
