using System;
using System.Collections.Generic;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Game.Interface.GameObjects
{
    public interface IGameObjectManager
    {
        event EventHandler<GameObjectsSynchronizedEventArgs> Synchronized;

        IReadOnlyCollection<IGameObject> GameObjects { get; }
    }
}
