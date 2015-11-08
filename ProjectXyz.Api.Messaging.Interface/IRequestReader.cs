using System;
using System.IO;

namespace ProjectXyz.Api.Messaging.Interface
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
