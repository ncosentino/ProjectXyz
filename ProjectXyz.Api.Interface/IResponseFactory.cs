using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Interface
{
    public interface IResponseFactory
    {
        #region Methods
        TResponse Create<TResponse>(
            Guid requestId,
            Action<TResponse> callback)
            where TResponse : IResponse;
        #endregion
    }
}