using System;
using System.Collections.Generic;

namespace ProjectXyz.Api.GameObjects
{
    public interface IGameObjectManager
    {
        event EventHandler<GameObjectsSynchronizedEventArgs> Synchronized;

        IReadOnlyCollection<IGameObject> GameObjects { get; }
    }
}
