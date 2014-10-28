using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using NUnit.Framework;
using System.Threading;
using MvvmUserApp.Resources;

namespace MvvmUserApp.Spec
{
    [Binding]
    public class UserAppSteps
    {
        private Process _queue;
        private Application _userApp;
        private Window _userWindow;

        public const string BtnEnqueueName = "btnEnqueu";
        public const string BtnInAct = "btnInAct";
        public const string BtnOutAct = "btnOutAct";
        public const string TextID = "TextInfo";
        public const string UserAppMainWindowTitle = "MVVM Nishtiak User Application";

        public const int WaitOperationMiliseconds = 3000;

        [Given(@"I have started (.*) and (.*)")]
        public void GivenIHaveStarted(string queuPath, string userAppPath)
        {
            _queue = Process.Start(queuPath);
            if (userAppPath != "-1")
            {
                var userApp = Process.Start(userAppPath);
                _userApp = Application.Attach(userApp);
                _userWindow = _userApp.GetWindow(UserAppMainWindowTitle);
                Thread.Sleep(1000);
            }
        }

        [When(@"I press stay in queue")]
        public void WhenIPressStayInQueue()
        {
            CheckLabelText(ResourceStrings.ServerReady);
            var btnEnqueu = _userWindow.Get<Button>(BtnEnqueueName);
            btnEnqueu.Click();
        }

        [When(@"I press use (.*) nishtiak")]
        public void WhenIPressUseNishtiak(bool use)
        {
            CheckLabelText(ResourceStrings.OfferToUse);

            Button btnAct = null;

            if (use)
            {
                btnAct = _userWindow.Get<Button>(BtnInAct);
            }
            else
            {
                btnAct = _userWindow.Get<Button>(BtnOutAct);
            }

            btnAct.Click();
        }

        [When(@"I press stop use nishtiak")]
        public void WhenIPressStopUseNishtiak()
        {
            CheckLabelText(ResourceStrings.UseNishtiak);
            _userWindow.Get<Button>(BtnOutAct).Click();
        }

        [Then(@"I must see start page")]
        public void ThenIMustSeeStartPage()
        {
            CheckLabelText(ResourceStrings.ServerReady);
        }

        private void CheckLabelText(string text)
        {
            int step = 10;

            Label lbl = null;

            for (int i = 0; i < WaitOperationMiliseconds / step; i++)
            {
                try
                {
                    lbl = _userWindow.Get<Label>(TextID);

                    if (lbl.Text == text)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(step);
                    }
                }
                catch
                {
                    Thread.Sleep(step);
                }
            }

            Assert.AreEqual(lbl.Text, text);
        }

        [AfterScenario]
        public void KillProcesses()
        {
            if (_queue != null)
            {
                _queue.Kill();
            }

            if (_userApp != null)
            {
                _userApp.Kill();
            }
            _queue = null;
            _userApp = null;
            _userWindow = null;
        }

    }
}
