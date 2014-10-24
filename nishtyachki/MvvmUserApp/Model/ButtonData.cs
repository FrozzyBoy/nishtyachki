using MvvmUserApp.QueueReference;
using MvvmUserApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MvvmUserApp.Model
{
    public enum ButtonsState
    {
        Online = 0,
        InQueue,
        AcceptUse,
        Use
    }

    public enum ButtonType
    {
        Enqueue = 0,
        In,
        Out
    }

    public class ButtonData : IButtonData
    {
        private static IButtonData[] AllButtons;

        private ButtonData(Visibility visibility, string content, ICommand command)
        {
            this.Visibility = visibility;
            this.Content = content;
            this.Command = command;
        }

        private static int GetIndexOfElementInList(ButtonsState state, ButtonType type)
        {
            int index = 0;

            int btnStateLength = Enum.GetNames(typeof(ButtonsState)).Length;
            index = btnStateLength * (int)type + (int)state;

            return index;
        }

        public static IButtonData GetButtonOfType(ButtonsState state, ButtonType type)
        {
            var index = GetIndexOfElementInList(state, type);
            var result = AllButtons[index];

            if (result == null)
            {
                AllButtons[index] = new ButtonData(Visibility.Hidden, "", new ExecutableActionCommand(() => { }));
                result = AllButtons[index];
            }

            return result;
        }

        #region Impliment IButtonData Interface
        public Visibility Visibility
        {
            get;
            private set;
        }

        public string Content
        {
            get;
            private set;
        }

        public ICommand Command
        {
            get;
            private set;
        }
        #endregion Impliment IButtonData Interface

        internal static void InitCommandsForButtons(IUserAppService service)
        {
            int count = Enum.GetNames(typeof(ButtonType)).Length * Enum.GetNames(typeof(ButtonsState)).Length;
            IButtonData[] arrayOfButtons = new IButtonData[count];

            ExecutableActionCommand answerPositive = new ExecutableActionCommand(() =>
                {
                    service.AnswerForOfferToUseAsync(true);
                });

            ExecutableActionCommand answerNegative = new ExecutableActionCommand(() =>
                {
                    service.AnswerForOfferToUseAsync(false);
                });

            ExecutableActionCommand leaveQueue = new ExecutableActionCommand(() =>
            {
                service.LeaveQueueAsync();
            });

            ExecutableActionCommand stopUse = new ExecutableActionCommand(() =>
                {
                    service.StopUseObjAsync();
                });

            ExecutableActionCommand standInQueue = new ExecutableActionCommand(() =>
            {
                service.TryStandInQueueAsync();
            });

            arrayOfButtons[GetIndexOfElementInList(ButtonsState.Online, ButtonType.Enqueue)]
                = new ButtonData(Visibility.Visible, ResourceStrings.BtnEnqueuEneble, standInQueue);

            arrayOfButtons[GetIndexOfElementInList(ButtonsState.InQueue, ButtonType.Out)]
                = new ButtonData(Visibility.Visible, ResourceStrings.BtnCancelEnabled, leaveQueue);

            arrayOfButtons[GetIndexOfElementInList(ButtonsState.AcceptUse, ButtonType.In)]
                = new ButtonData(Visibility.Visible, ResourceStrings.BtnYesEnabled, answerPositive);

            arrayOfButtons[GetIndexOfElementInList(ButtonsState.AcceptUse, ButtonType.Out)]
                = new ButtonData(Visibility.Visible, ResourceStrings.BtnNoEnabled, answerNegative);

            arrayOfButtons[GetIndexOfElementInList(ButtonsState.Use, ButtonType.Out)]
                = new ButtonData(Visibility.Visible, ResourceStrings.BtnCancelEnabled, stopUse);

            AllButtons = arrayOfButtons;
        }
    }
}
