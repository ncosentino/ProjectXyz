using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Interface
{
    public interface IRequestRegistrar : IDisposable
    {
        #region Methods
        void Subscribe<TRequest>(Action<TRequest> handler) 
            where TRequest : IRequest;

        void Unsubscribe<TRequest>(Action<TRequest> handler)
            where TRequest : IRequest;
        #endregion
    }
}
