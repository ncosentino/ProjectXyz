using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
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

        public IReadOnlyDictionary<IIdentifier, int> TotalSockets =>
            _totalSocketTypes
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());

        public IReadOnlyDictionary<IIdentifier, int> AvailableSockets =>
            _totalSocketTypes
                .GroupBy(x => x)
                .ToDictionary(
                    x => x.Key,
                    x => x.Count() - _socketed.Count(s => s.SocketType.Equals(x.Key)));

        public bool CanFitSocket(ICanFitSocketBehavior canFitSocket)
        {
            if (!AvailableSockets.TryGetValue(
                canFitSocket.SocketType,
                out var freeSocketsOfType))
            {
                return false;
            }

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