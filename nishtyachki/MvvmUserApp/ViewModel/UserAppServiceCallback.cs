using MvvmUserApp.Model;
using MvvmUserApp.QueueReference;
using MvvmUserApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MvvmUserApp.ViewModel
{
    public class UserAppServiceCallback : IUserAppServiceCallback
    {
        public IMainViewModel MainViewModel { get; set; }

        private IShowMessages _show;

        public UserAppServiceCallback(IShowMessages show)
        {
            _show = show;
        }

        public void NotifyServerReady()
        {
            this.MainViewModel.Text = ResourceStrings.ServerReady;
            this.MainViewModel.BtnEnqueue = ButtonData.GetButtonOfType(ButtonsState.Online, ButtonType.Enqueue);
            this.MainViewModel.BtnIn = ButtonData.GetButtonOfType(ButtonsState.Online, ButtonType.In);
            this.MainViewModel.BtnOut = ButtonData.GetButtonOfType(ButtonsState.Online, ButtonType.Out);
        }

        public void ShowMessage(string text)
        {
            _show.Show(text);
        }

        public void StandInQueue()
        {
            this.MainViewModel.Text = ResourceStrings.StayInQueue;
            this.MainViewModel.BtnEnqueue = ButtonData.GetButtonOfType(ButtonsState.InQueue, ButtonType.Enqueue);
            this.MainViewModel.BtnIn = ButtonData.GetButtonOfType(ButtonsState.InQueue, ButtonType.In);
            this.MainViewModel.BtnOut = ButtonData.GetButtonOfType(ButtonsState.InQueue, ButtonType.Out);
        }

        public void ShowPosition(int position)
        {
            this.MainViewModel.Text = string.Format(ResourceStrings.PositionInQueue, position);
        }

        public void OfferToUseObj()
        {
            this.MainViewModel.Text = ResourceStrings.OfferToUse;
            this.MainViewModel.BtnEnqueue = ButtonData.GetButtonOfType(ButtonsState.AcceptUse, ButtonType.Enqueue);
            this.MainViewModel.BtnIn = ButtonData.GetButtonOfType(ButtonsState.AcceptUse, ButtonType.In);
            this.MainViewModel.BtnOut = ButtonData.GetButtonOfType(ButtonsState.AcceptUse, ButtonType.Out);
        }

        public void NotifyToUseObj()
        {
            this.MainViewModel.Text = ResourceStrings.UseNishtiak;
            this.MainViewModel.BtnEnqueue = ButtonData.GetButtonOfType(ButtonsState.Use, ButtonType.Enqueue);
            this.MainViewModel.BtnIn = ButtonData.GetButtonOfType(ButtonsState.Use, ButtonType.In);
            this.MainViewModel.BtnOut = ButtonData.GetButtonOfType(ButtonsState.Use, ButtonType.Out);
        }

        public void DroppedByServer(string text)
        {
            ShowMessage(text);
            NotifyServerReady();
        }

    }
}
