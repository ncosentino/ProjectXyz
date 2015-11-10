using System;
using System.Collections.Generic;
using ProjectXyz.Api.Messaging.Core;
using ProjectXyz.Api.Messaging.Core.Maps;

namespace ProjectXyz.Api.Messaging.Interface.Maps
{
    public sealed class StaticMapDataResponse : BaseResponse
    {
        #region Properties
        public Guid MapId { get; set; }

        public IEnumerable<MapTile> Terrain { get; set; }
        #endregion
    }
}
