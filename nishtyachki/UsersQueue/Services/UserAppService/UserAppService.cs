using System;
using System.ServiceModel;
using System.Threading;
using UsersQueue.Queue;
using UsersQueue.Queue.UserInformtion;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace UsersQueue.Services.UserAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserAppService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserAppService : IUserAppService, IDisposable
    {
        private QueueUser _user;
        private bool _isDisconnected = false;

        private static ConcurrentDictionary<string, IUserAppServiceClient> _clients = new ConcurrentDictionary<string,IUserAppServiceClient>();

        public void InitUser()
        {
            _isDisconnected = false;

            IUserAppServiceClient client = OperationContext.Current.GetCallbackChannel<IUserAppServiceClient>();
            string key = Thread.CurrentPrincipal.Identity.Name;
            key = key.Replace('\\', '_');
            key = key.ToUpper();

            IUserAppServiceClient safeClient = new ClientSafalyCommunicate(client);

            _user = QueueUser.GetOrInitQueueUser(key, safeClient);
            _user.ChangedState += ChangeStateThenNotifyClient;

            _clients[key] = safeClient;

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
                    _clients[_user.ID].NotifyServerReady();
                    break;
                case UserCurrentState.InQueue:
                    _clients[_user.ID].StandInQueue();
                    break;
                case UserCurrentState.AcceptingOffer:
                    _clients[_user.ID].OfferToUseObj();
                    break;
                case UserCurrentState.UsingNishtiak:
                    _clients[_user.ID].NotifyToUseObj();
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
                    _user.ChangedState -= ChangeStateThenNotifyClient;
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
