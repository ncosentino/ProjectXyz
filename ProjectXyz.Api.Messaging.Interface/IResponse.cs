using System;

namespace ProjectXyz.Api.Messaging.Interface
{
    public interface IResponse : IIPayload
    {
        #region Properties
        Guid RequestId { get; }
        #endregion
    }
}
