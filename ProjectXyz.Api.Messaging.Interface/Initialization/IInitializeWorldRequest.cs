using System;

namespace ProjectXyz.Api.Messaging.Interface.Initialization
{
    public interface IInitializeWorldRequest : IRequest
    {
        #region Properties
        Guid PlayerId { get; }
        #endregion
    }
}