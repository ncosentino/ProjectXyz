using System;

namespace ProjectXyz.Api.Messaging.Core.Maps
{
    public sealed class MapTile
    {
        #region Properties
        public Location Position { get; set; }

        public Guid GraphicResourceId { get; set; }
        #endregion
    }
}
