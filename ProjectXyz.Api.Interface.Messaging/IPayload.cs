using System;
using System.IO;

namespace ProjectXyz.Api.Interface.Messaging
{
    public interface IIPayload
    {
        #region Properties
        Guid Id { get; }
        #endregion

        #region Methods
        void Write(Stream stream);
        #endregion
    }
}
