using System;

namespace ProjectXyz.Api.Messaging.Interface.Maps
{
    public interface IMapDataRequest : IRequest
    {
        #region Properties
        Guid MapId { get; }
        #endregion
    }
}
