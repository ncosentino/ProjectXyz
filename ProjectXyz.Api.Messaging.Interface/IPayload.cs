using System;
using System.IO;

namespace ProjectXyz.Api.Messaging.Interface
{
    public interface IPayload
    {
        #region Properties
        Guid Id { get; }
        #endregion
    }
}
