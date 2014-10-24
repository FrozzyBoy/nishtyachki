using MvvmUserApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmUserApp.ViewModel
{
    public interface IMainViewModel
    {
        IButtonData BtnIn { get; set; }
        IButtonData BtnOut { get; set; }
        IButtonData BtnEnqueue { get; set; }
        string Text { get; set; }
    }
}
