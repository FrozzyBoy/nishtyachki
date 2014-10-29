using System;
using System.ServiceModel;
using System.Threading;
using UsersQueue.Queue;
using UsersQueue.Queue.UserInformtion;
using System.Collections.Generic;

namespace UsersQueue.Services.UserAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserAppService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserAppService : IUserAppService, IDisposable
    {
        private QueueUser _user;

        private bool _isDisconnected = false;

        public void InitUser()
        {
            _isDisconnected = false;

            IUserAppServiceClient client = OperationContext.Current.GetCallbackChannel<IUserAppServiceClient>();
            string key = Thread.CurrentPrincipal.Identity.Name;
            key = key.Replace('\\', '_');
            key = key.ToUpper();

            IUserAppServiceClient safeClient = new ClientSafalyCommunicate(client);

            _user = new QueueUser(key, safeClient);
            _user.ChangedState += ChangeStateThenNotifyClient;
            ChangeStateThenNotifyClient();
        }

        private void ChangeStateThenNotifyClient()
        {
            switch (_user.State)
            {
                case UserCurrentState.Offline:
                    _user.State = UserCurrentState.Online;
                    break;
                case UserCurrentState.Online:
                    _user.Client.NotifyServerReady();
                    break;
                case UserCurrentState.InQueue:
                    _user.Client.StandInQueue();
                    break;
                case UserCurrentState.AcceptingOffer:
                    _user.Client.OfferToUseObj();
                    break;
                case UserCurrentState.UsingNishtiak:
                    _user.Client.NotifyToUseObj();
                    break;
                default:
                    break;
            }

        }

        public bool TryStandInQueue()
        {
            bool stay = UsersQueueInstance.Instance.AddUserInQueue(_user);

            return stay;
        }

        public void LeaveQueue()
        {
            UsersQueueInstance.Instance.DeleteFromTheQueue(_user);
        }

        public void AnswerForOfferToUse(bool willUse)
        {
            if (willUse)
            {
                UsersQueueInstance.Instance.StartUseNishtiak(_user);
            }
            else
            {
                UsersQueueInstance.Instance.DeleteFromTheQueue(_user);
            }
        }

        public void StopUseObj()
        {
            UsersQueueInstance.Instance.EndUseNishtiak(_user);
        }

        public void Disconnect()
        {
            if (_isDisconnected)
            {
                _isDisconnected = true;

                _user.State = UserCurrentState.Offline;
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_user != null && _user.Client != null)
                {
                    _user.Client.DroppedByServer("stop your session");
                }

                Disconnect();
                _user = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~UserAppService()
        {
            Dispose(false);
        }
    }
}
