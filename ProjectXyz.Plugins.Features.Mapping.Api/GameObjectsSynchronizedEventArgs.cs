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
            IEnumerable<IGameObject> removed)
        {
            Added = added.ToArray();
            Removed = removed.ToArray();
        }

        public IReadOnlyCollection<IGameObject> Added { get; }

        public IReadOnlyCollection<IGameObject> Removed { get; }
    }
}