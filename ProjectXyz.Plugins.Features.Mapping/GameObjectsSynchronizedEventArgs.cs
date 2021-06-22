using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public sealed class GameObjectsSynchronizedEventArgs : EventArgs
    {
        public GameObjectsSynchronizedEventArgs(
            IEnumerable<IGameObject> added,
            IEnumerable<IGameObject> removed,
            IReadOnlyCollection<IGameObject> immutableFullSet)
        {
            Added = added.ToArray();
            Removed = removed.ToArray();
            ImmutableFullSet = immutableFullSet;
        }

        public IReadOnlyCollection<IGameObject> Added { get; }

        public IReadOnlyCollection<IGameObject> Removed { get; }

        public IReadOnlyCollection<IGameObject> ImmutableFullSet { get; }
    }
}