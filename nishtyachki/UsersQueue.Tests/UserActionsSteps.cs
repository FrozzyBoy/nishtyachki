using Moq;
using Moq.Language.Flow;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using TechTalk.SpecFlow;
using UsersQueue.Tests.UserAppService;

namespace UsersQueue.Tests
{
    [Binding]
    public class UserActionsSteps
    {
        public enum UserState
        {
            InQueue,
            OutOfQueue
        }

        private Process _nishtiakQueue;

        private UserAppServiceClient AppService
        {
            get
            {
                return ScenarioContext.Current["AppService"] as UserAppServiceClient;
            }
            set
            {
                ScenarioContext.Current.Add("AppService", value);
            }
        }

        Mock<IUserAppServiceCallback> callback;

        [Given(@"I have run queue (.*)")]
        public void GivenIHaveRunQueue(string pathToExe)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo(pathToExe);
            startinfo.CreateNoWindow = false;
            startinfo.WindowStyle = ProcessWindowStyle.Minimized;

            _nishtiakQueue = Process.Start(startinfo);
        }

        [Given(@"I have been connect to server")]
        public void GivenIHaveBeenConnectToServer()
        {
            Thread.Sleep(500);

            callback = new Mock<IUserAppServiceCallback>();
            var ic = new InstanceContext(callback.Object);
            AppService = new UserAppServiceClient(ic);
            AppService.InitUser();
        }

        [When(@"I press (.*) in queue")]
        public void WhenIPressInQueue(UserState state)
        {
            var task = AppService.TryStandInQueueAsync();

            var action = callback.Setup(c => c.StandInQueue());
            WaitAsyncOperation<IUserAppServiceCallback>(action, 10000);

            ScenarioContext.Current["tryStandInQueueResult"] = task.Result ? UserState.InQueue : UserState.OutOfQueue;
        }

        private void WaitAsyncOperation<T>(ISetup<T> action, int timeout)
            where T : class
        {
            EventWaitHandle callback = new ManualResetEvent(false);

            action.Callback(() =>
            {
                callback.Set();
            });

            callback.WaitOne(timeout);
        }

        [Then(@"the result should (.*)")]
        public void ThenTheResultShould(UserState state)
        {
            callback.Verify(c => c.StandInQueue(), Times.Once);

            Assert.AreEqual(state, (UserState)ScenarioContext.Current["tryStandInQueueResult"]);
        }

        [When(@"I press to use nishtiak")]
        public void WhenIPressToUseNishtiak()
        {
            AppService.AnswerForOfferToUse(true);

        }

        [Then(@"I must be notifyid to use it")]
        public void ThenIMustBeNotifyidToUseIt()
        {
            callback.Verify(c => c.NotifyToUseObj(), Times.Once);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _nishtiakQueue.Kill();
            _nishtiakQueue = null;
        }
    }
}
