using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Game.Core.Behaviors
{
    public sealed class CanBeSocketedBehavior :
        BaseBehavior,
        ICanBeSocketedBehavior
    {
        private readonly IReadOnlyCollection<IIdentifier> _totalSocketTypes;
        private readonly List<ICanFitSocketBehavior> _socketed;

        public CanBeSocketedBehavior(IEnumerable<IIdentifier> totalSocketTypes)
        {
            _totalSocketTypes = totalSocketTypes.ToArray();
            _socketed = new List<ICanFitSocketBehavior>();
        }

        public event EventHandler<EventArgs<Tuple<ICanBeSocketedBehavior, ICanFitSocketBehavior>>> Socketed;

        public IReadOnlyCollection<ICanFitSocketBehavior> OccupiedSockets => _socketed;

        public bool CanFitSocket(ICanFitSocketBehavior canFitSocket)
        {
            var totalSocketsOfType = _totalSocketTypes.Count(x => Equals(x, canFitSocket.SocketType));
            var usedSocketsOfType = _socketed
                .Where(x => x.SocketType.Equals(canFitSocket.SocketType))
                .Sum(x => x.SocketSize);
            var freeSocketsOfType = totalSocketsOfType - usedSocketsOfType;
            return freeSocketsOfType >= canFitSocket.SocketSize;
        }

        public bool Socket(ICanFitSocketBehavior canFitSocket)
        {
            if (!CanFitSocket(canFitSocket))
            {
                return false;
            }

            _socketed.Add(canFitSocket);
            Socketed?.Invoke(
                this,
                new EventArgs<Tuple<ICanBeSocketedBehavior, ICanFitSocketBehavior>>(new Tuple<ICanBeSocketedBehavior, ICanFitSocketBehavior>(
                    this,
                    canFitSocket)));
            return true;
        }
    }
}