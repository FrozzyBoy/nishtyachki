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
