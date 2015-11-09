using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Interface
{
    public interface IResponseSender
    {
        #region Methods
        void Send<TResponse>(TResponse response)
            where TResponse : IResponse;
        #endregion
    }
}
