using System;

namespace ProjectXyz.Api.Messaging.Core.GameObjects
{
    public sealed class GameObjectPath
    {
        #region Properties
        public Guid MapId { get; set; }

        public Guid GameObjectId { get; set; }
        #endregion
    }
}
