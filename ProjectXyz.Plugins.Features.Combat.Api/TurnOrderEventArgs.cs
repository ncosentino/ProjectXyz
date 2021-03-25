using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public sealed class TurnOrderEventArgs : EventArgs
    {
        public TurnOrderEventArgs(IEnumerable<IGameObject> actorOrder)
        {
            ActorOrder = actorOrder.ToArray();
        }

        public IReadOnlyCollection<IGameObject> ActorOrder { get; }
    }
}
