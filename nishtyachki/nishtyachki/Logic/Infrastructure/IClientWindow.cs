using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nishtyachki.Logic.Infrastructure
{
    public interface IClientWindow
    {
        void ShowMessage(string msg);
        void NotifyServerReady();
        void NotifyToUseObj();
        void StandInQueue();
        void OfferToUseObj();
        void AnswerForOffer(bool willUse);
        void LeaveQueue();
        void StopUse();
        void DroppedByServer(string text);
    }
}
