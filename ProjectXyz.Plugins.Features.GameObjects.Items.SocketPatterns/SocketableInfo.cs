using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class SocketableInfo : ISocketableInfo
    {
        public SocketableInfo(
            IGameObject item,
            ICanBeSocketedBehavior canBeSocketedBehavior)
        {
            Item = item;
            CanBeSocketedBehavior = canBeSocketedBehavior;
            TotalSockets = canBeSocketedBehavior.TotalSockets;
            AvailableSockets = canBeSocketedBehavior.AvailableSockets;
            OccupiedSockets = canBeSocketedBehavior.OccupiedSockets.ToArray();
            TotalSocketCount = TotalSockets.Sum(x => x.Value);
            AvailableSocketCount = AvailableSockets.Sum(x => x.Value);
        }

        public IGameObject Item { get; }

        public ICanBeSocketedBehavior CanBeSocketedBehavior { get; }

        public IReadOnlyDictionary<IIdentifier, int> TotalSockets { get; }

        public IReadOnlyDictionary<IIdentifier, int> AvailableSockets { get; }

        public IReadOnlyList<ICanFitSocketBehavior> OccupiedSockets { get; }

        public int TotalSocketCount { get; }

        public int AvailableSocketCount { get; }
    }
}
