using System;
using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IReadOnlyMapGameObjectManager
    {
        event EventHandler<GameObjectsSynchronizedEventArgs> Synchronized;

        IReadOnlyCollection<IGameObject> GameObjects { get; }
    }
}
