using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public sealed class GameObjectsSynchronizedEventArgs : EventArgs
    {
        public GameObjectsSynchronizedEventArgs(
            IEnumerable<IGameObject> added,
            IEnumerable<IGameObject> removed,
            IEnumerable<IGameObject> fullSet)
        {
            Added = added.ToArray();
            Removed = removed.ToArray();
            FullSet = fullSet.ToArray();
        }

        public IReadOnlyCollection<IGameObject> Added { get; }

        public IReadOnlyCollection<IGameObject> Removed { get; }

        public IReadOnlyCollection<IGameObject> FullSet { get; }
    }
}