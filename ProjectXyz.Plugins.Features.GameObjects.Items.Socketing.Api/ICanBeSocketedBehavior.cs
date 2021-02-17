﻿using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Events;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api
{
    public interface ICanBeSocketedBehavior : IBehavior
    {
        event EventHandler<EventArgs<Tuple<ICanBeSocketedBehavior, ICanFitSocketBehavior>>> Socketed;

        IReadOnlyCollection<ICanFitSocketBehavior> OccupiedSockets { get; }

        IReadOnlyDictionary<IIdentifier, int> TotalSockets { get; }

        IReadOnlyDictionary<IIdentifier, int> AvailableSockets { get; }

        bool CanFitSocket(ICanFitSocketBehavior canFitSocket);

        bool Socket(ICanFitSocketBehavior canFitSocket);
    }
}