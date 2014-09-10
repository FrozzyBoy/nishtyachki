using System;
using System.ServiceModel;
using AdminApp;
using AdminApp.Queue;
using System.Threading;
using AdminApp.Services;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfCommunicator" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfCommunicator.svc or WcfCommunicator.svc.cs at the Solution Explorer and start debugging.
    [ServiceContract(CallbackContract = typeof(IClient))]
    public class WcfCommunicator : IWcfService
    {
        private User _user;

        private bool _isDisconnected = false;

        public void InitUser()
        {            
            _isDisconnected = false;
            
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            string key = Thread.CurrentPrincipal.Identity.Name;

            key = key.Replace('\\', '_');

            AdminApp.Services.IClient safeClient = new ClientSafalyCommunicate(client);
            _user = new User(key, safeClient);

            safeClient.NotifyServerReady();
        }

        public bool TryStandInQueue()
        {
            bool stay = UsersQueue.Instance.AddUserInQueue(_user);

            return stay;
        }

        public void LeaveQueue()
        {
            UsersQueue.Instance.DeleteFromTheQueue(_user);
        }

        public void AnswerForOfferToUse(bool willUse)
        {
            if (willUse)
            {
                UsersQueue.Instance.StartUseNishtiak(_user);
            }
            else
            {
                UsersQueue.Instance.DeleteFromTheQueue(_user);
            }
        }

        public void StopUseObj()
        {
            UsersQueue.Instance.EndUseNishtiak(_user);
        }
                
        public void Disconnect()
        {
            if (_isDisconnected)
            {
                _isDisconnected = true;

                _user.State = UserState.Offline;
                
                try
                {
                    LeaveQueue();
                }
                catch (Exception)
                {
                }

                try
                {
                    StopUseObj();
                }
                catch (Exception)
                {
                }
            }
            
        }
    }
}
