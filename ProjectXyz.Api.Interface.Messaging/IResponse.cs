using System;

namespace ProjectXyz.Api.Interface.Messaging
{
    public interface IResponse : IIPayload
    {
        #region Properties
        Guid RequestId { get; }
        #endregion
    }
}
