using System;
using System.IO;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Serialization.Interface
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
