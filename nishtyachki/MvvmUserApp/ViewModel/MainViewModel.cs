using GalaSoft.MvvmLight;
using MvvmUserApp.Model;
using System.Threading;
using System.Windows.Input;
using MvvmUserApp.QueueReference;
using MvvmUserApp.Resources;
using System.Windows;
using MvvmUserApp.Views;

namespace MvvmUserApp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private readonly IUserAppService _dataService;
        private readonly IStateChangerWindow _view;

        private IButtonData _btnIn;
        public IButtonData BtnIn
        {
            get
            {
                return _btnIn;
            }
            set
            {
                base.Set<IButtonData>(ref _btnIn, value);
            }
        }

        private IButtonData _btnOut;
        public IButtonData BtnOut
        {
            get
            {
                return _btnOut;
            }
            set
            {
                base.Set<IButtonData>(ref _btnOut, value);
            }
        }

        private IButtonData _btnEnqueue;
        public IButtonData BtnEnqueue
        {
            get
            {
                return _btnEnqueue;
            }
            set
            {
                base.Set<IButtonData>(ref _btnEnqueue, value);
            }
        }

        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                base.Set<string>(ref _text, value);
            }
        }

        public ICommand HideApp
        {
            get
            {
                return new ExecutableActionCommand(()=>
                {
                    _view.Hide();
                });
            }
        }

        public ICommand CloseApp
        {
            get
            {
                return new ExecutableActionCommand(() =>
                {
                    _view.Close();
                });
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IUserAppService dataService, IStateChangerWindow view)
        {
            Text = ResourceStrings.StartMessage;
            _dataService = dataService;
            _dataService.InitUserAsync();
            _view = view;

            //_dataService = dataService;
        }

        //public override void Cleanup()
        //{
        //    // clean up if needed

        //    base.Cleanup();
        //}

    }
}