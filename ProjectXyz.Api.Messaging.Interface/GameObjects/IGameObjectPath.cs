using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects
{
    public interface IGameObjectPath
    {
        #region Properties
        Guid MapId { get; }

        Guid GameObjectId { get; }
        #endregion
    }
}
