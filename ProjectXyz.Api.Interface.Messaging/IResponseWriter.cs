using System.IO;

namespace ProjectXyz.Api.Messaging.Interface
{
    public interface IResponseWriter
    {
        #region Methods
        void Write<TResponse>(TResponse response, Stream stream)
            where TResponse : IResponse;
        #endregion
    }
}
