using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MvvmUserApp.Model
{
    public interface IButtonData
    {
        Visibility Visibility { get; }
        string Content { get; }
        ICommand Command { get; }
    }
}
