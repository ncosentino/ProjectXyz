using System;
using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Interface.GameObjects
{
    public interface IGameObjectRepository
    {
        IEnumerable<IGameObject> LoadForMap(IIdentifier mapId);
    }
}
