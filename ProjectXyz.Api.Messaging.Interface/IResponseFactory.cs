using System;

namespace ProjectXyz.Api.Messaging.Interface
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