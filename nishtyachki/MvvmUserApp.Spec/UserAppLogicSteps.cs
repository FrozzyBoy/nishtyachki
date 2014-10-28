using Moq;
using MvvmUserApp.Model;
using MvvmUserApp.QueueReference;
using MvvmUserApp.Resources;
using MvvmUserApp.ViewModel;
using NUnit.Framework;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using TechTalk.SpecFlow;

namespace MvvmUserApp.Spec
{
    [Binding]
    public class UserAppLogicSteps
    {
        private Mock<UserAppService.IUserAppServiceCallback> _callback = new Mock<UserAppService.IUserAppServiceCallback>();
        private UserAppServiceCallback _callbackViewModel;
        private MainViewModel _mainModel;
        private Mock<Views.IStateChangerWindow> _view = new Mock<Views.IStateChangerWindow>();

        private Process _queue;

        public const string QueuePath = "D:/Projects/nishtyachki/nishtyachki/WinServiceHostUsersQueue/bin/Debug/WinServiceHostUsersQueue.exe";

        [AfterScenario]
        public void AfterScenario()
        {
            if (_queue != null)
            {
                _queue.Kill();
            }

            _queue = null;

        }

        [Given(@"I have start queue")]
        public void GivenIHaveStartQueue()
        {
            _queue = Process.Start(QueuePath);
            Thread.Sleep(1000);
        }

        [Then(@"I must be on start page")]
        public void ThenIMustBeOnStartPage()
        {
            _callback.Verify(c => c.NotifyServerReady(), Times.Exactly(2));
        }

        [Then(@"I must be notified that i`m in queue")]
        public void ThenIMustBeNotifiedThatIMInQueue()
        {
            _callback.Verify(obj => obj.StandInQueue(), Times.Once());
        }

        [Then(@"Buttons state must be changed to enqueu state")]
        public void ThenButtonsStateMustBeChangedToEnqueueState()
        {
            Assert.IsTrue(_mainModel.Text == ResourceStrings.StayInQueue);
            Assert.IsTrue(_mainModel.BtnIn.Visibility == Visibility.Hidden);
            Assert.IsTrue(_mainModel.BtnOut.Visibility == Visibility.Visible);
            Assert.IsTrue(_mainModel.BtnEnqueue.Visibility == Visibility.Hidden);
        }

        [Given(@"I have init callback")]
        public void GivenIHaveInitCallback()
        {
            var show = (new Mock<IShowMessages>()).Object;
            _callbackViewModel = new UserAppServiceCallback(show);

            _mainModel = new MainViewModel(null, _view.Object);

            _callbackViewModel.MainViewModel = _mainModel;
            var service = new Mock<IUserAppService>();
            ButtonData.InitCommandsForButtons(service.Object);
        }

        [When(@"I say to callback that i stay in queue")]
        public void WhenISayToCallbackThatIStayInQueue()
        {
            _callbackViewModel.StandInQueue();
        }

        [When(@"I say to callback that i mist be offered to use nishtiak")]
        public void WhenISayToCallbackThatIMistBeOfferedToUseNishtiak()
        {
            _callbackViewModel.OfferToUseObj();
        }

        [Then(@"Buttons state must be changed to offer nishtiak state")]
        public void ThenButtonsStateMustBeChangedToOfferNishtiakState()
        {
            Assert.IsTrue(_mainModel.Text == ResourceStrings.OfferToUse);
            Assert.IsTrue(_mainModel.BtnEnqueue.Visibility == Visibility.Hidden);
            Assert.IsTrue(_mainModel.BtnIn.Visibility == Visibility.Visible);
            Assert.IsTrue(_mainModel.BtnIn.Content == ResourceStrings.BtnYesEnabled);
            Assert.IsTrue(_mainModel.BtnOut.Visibility == Visibility.Visible);
            Assert.IsTrue(_mainModel.BtnOut.Content == ResourceStrings.BtnNoEnabled);
        }

        [When(@"I say to callback that i offered to use nistiak")]
        public void WhenISayToCallbackThatIOfferedToUseNistiak()
        {
            _callbackViewModel.NotifyToUseObj();
        }

        [Then(@"Buttons state must be changed using nishtiak")]
        public void ThenButtonsStateMustBeChangedUsingNishtiak()
        {
            Assert.IsTrue(_mainModel.Text == ResourceStrings.UseNishtiak);
            Assert.IsTrue(_mainModel.BtnEnqueue.Visibility == Visibility.Hidden);
            Assert.IsTrue(_mainModel.BtnIn.Visibility == Visibility.Hidden);
            Assert.IsTrue(_mainModel.BtnOut.Visibility == Visibility.Visible);
            Assert.IsTrue(_mainModel.BtnOut.Content == ResourceStrings.BtnCancelEnabled);
        }


    }
}
