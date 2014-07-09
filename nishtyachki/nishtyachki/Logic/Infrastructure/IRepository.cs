using System;
namespace nishtyachki.Logic.Infrastructure
{
    public interface IRepository
    {
        bool SendRequest();
        int NumberOfPeopleInFrontOfMe { get; }
    }
}
