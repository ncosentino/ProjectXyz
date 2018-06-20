using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Api.GameObjects
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