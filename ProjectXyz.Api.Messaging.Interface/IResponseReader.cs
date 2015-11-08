using System;
using System.IO;

namespace ProjectXyz.Api.Messaging.Interface
{
    public interface IResponseReader
    {
        #region Methods
        TResponse Read<TResponse>(Stream stream)
            where TResponse : IResponse;

        IResponse Read(Stream stream, Type type);
        #endregion
    }
}
