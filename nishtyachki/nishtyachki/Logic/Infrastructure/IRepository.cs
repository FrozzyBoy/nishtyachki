using System;
namespace nishtyachki.Logic.Infrastructure
{
    public interface IRepository
    {
        void StayInQueue();
        void LeaveQueue();
        void AnswerForOfferToUse(bool willUse);
    }
}
