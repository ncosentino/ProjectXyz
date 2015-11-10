using System;
using System.Collections.Generic;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.Maps
{
    public sealed class GameObjectMapDataResponse : BaseResponse
    {
        #region Properties
        public Guid MapId { get; set; }

        public IEnumerable<GameObject> Tiles { get; set; }
        #endregion
    }
}
