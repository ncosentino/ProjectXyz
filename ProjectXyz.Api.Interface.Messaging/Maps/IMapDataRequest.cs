using System;

namespace ProjectXyz.Api.Interface.Messaging.Maps
{
    public interface IMapDataRequest : IRequest
    {
        #region Properties
        Guid MapId { get; }
        #endregion
    }
}
