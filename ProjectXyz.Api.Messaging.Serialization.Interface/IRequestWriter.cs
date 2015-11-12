using System.IO;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Serialization.Interface
{
    public interface IRequestWriter
    {
        #region Methods
        void Write<TRequest>(TRequest requeste, Stream stream)
            where TRequest : IRequest;
        #endregion
    }
}
