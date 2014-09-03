using AdminApp.Queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace AdminApp.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfService.svc or WcfService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WcfService : IWcfService
    {
        private User _user;

        private bool _isDisconnected = false;

        public void InitUser()
        {
            _isDisconnected = false;
            
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            string key = Thread.CurrentPrincipal.Identity.Name;

            key = key.Replace('\\', '_');
            _user = new User(key, client);

            client.NotifyServerReady();
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
        
        ~WcfService()
        {
            Disconnect();
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
