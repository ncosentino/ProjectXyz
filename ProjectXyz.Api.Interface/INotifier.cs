using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Interface.Messaging;

namespace ProjectXyz.Api.Interface
{
    public interface INotifier
    {
        #region Methods
        void Notify<TNotification>(TNotification notification)
            where TNotification : INotification;
        #endregion
    }
}
