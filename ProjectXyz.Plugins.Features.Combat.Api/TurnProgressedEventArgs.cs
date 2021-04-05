using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Combat.Api
{
    public sealed class TurnProgressedEventArgs : EventArgs
    {
        public TurnProgressedEventArgs(
            IEnumerable<IGameObject> actorTurnProgression,
            IGameObject actorWithNextTurn)
        {
            ActorTurnProgression = actorTurnProgression.ToArray();
            ActorWithNextTurn = actorWithNextTurn;
        }

        public IReadOnlyCollection<IGameObject> ActorTurnProgression { get; }
        
        public IGameObject ActorWithNextTurn { get; }
    }
}
