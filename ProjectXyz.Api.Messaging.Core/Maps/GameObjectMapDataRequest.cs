using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.Maps
{
    public sealed  class GameObjectMapDataRequest : BaseRequest
    {
        #region Properties
        public Guid MapId { get; set; }
        #endregion
    }
}
