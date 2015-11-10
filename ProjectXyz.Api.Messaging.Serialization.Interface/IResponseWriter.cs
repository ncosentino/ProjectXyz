using System.IO;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Serialization.Interface
{
    public interface IResponseWriter
    {
        #region Methods
        void Write<TResponse>(TResponse response, Stream stream)
            where TResponse : IResponse;
        #endregion
    }
}
