using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public sealed class GameObjectsSynchronizedEventArgs : EventArgs
    {
        public GameObjectsSynchronizedEventArgs(
            IEnumerable<IGameObject> added,
            IEnumerable<IGameObject> removed,
            IFrozenCollection<IGameObject> allGameObjects)
        {
            Added = added.ToArray();
            Removed = removed.ToArray();
            AllGameObjects = allGameObjects;
        }

        public IReadOnlyCollection<IGameObject> Added { get; }

        public IReadOnlyCollection<IGameObject> Removed { get; }

        public IFrozenCollection<IGameObject> AllGameObjects { get; }
    }
}