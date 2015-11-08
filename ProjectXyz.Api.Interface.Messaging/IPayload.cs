using System;
using System.IO;

namespace ProjectXyz.Api.Messaging.Interface
{
    public interface IIPayload
    {
        #region Properties
        Guid Id { get; }

        string Type { get; }
        #endregion
    }
}
