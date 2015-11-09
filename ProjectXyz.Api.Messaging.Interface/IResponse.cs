using System;

namespace ProjectXyz.Api.Messaging.Interface
{
    public interface IResponse : IPayload
    {
        #region Properties
        Guid RequestId { get; }
        #endregion
    }
}
