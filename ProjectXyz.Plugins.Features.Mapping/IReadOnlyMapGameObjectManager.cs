using System;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IReadOnlyMapGameObjectManager
    {
        event EventHandler<GameObjectsSynchronizedEventArgs> Synchronized;

        bool IsSynchronizing { get; }

        IFrozenCollection<IGameObject> GameObjects { get; }
    }
}
