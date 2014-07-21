using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nishtyachki.Logic.Infrastructure
{
    public enum NotifyResult
    {
        Ok, Cancel, Nothing
    }

    public interface INotifyWindow
    {
        void ShowPosition(int pos);
        void SuggestToUseObject();
        NotifyResult Result { get; }
    }
}
