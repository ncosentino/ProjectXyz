using System;
using System.IO;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Serialization.Interface
{
    public interface IRequestReader
    {
        #region Methods
        TRequest Read<TRequest>(Stream stream)
            where TRequest : IRequest;

        IRequest Read(Stream stream, Type type);
        #endregion
    }
}
