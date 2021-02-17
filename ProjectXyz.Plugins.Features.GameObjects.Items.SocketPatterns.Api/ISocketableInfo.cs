using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api
{
    public interface ISocketableInfo
    {
        IReadOnlyDictionary<IIdentifier, int> AvailableSockets { get; }

        ICanBeSocketedBehavior CanBeSocketedBehavior { get; }

        IGameObject Item { get; }

        IReadOnlyList<ICanFitSocketBehavior> OccupiedSockets { get; }

        IReadOnlyDictionary<IIdentifier, int> TotalSockets { get; }

        int TotalSocketCount { get; }

        int AvailableSocketCount { get; }
    }
}
