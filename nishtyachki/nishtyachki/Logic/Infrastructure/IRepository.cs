using System;
namespace nishtyachki.Logic.Infrastructure
{
    public interface IRepository
    {
        bool SendRequest(string userID);
        int NumberOfPeopleInFrontOfMe { get; }
    }
}
