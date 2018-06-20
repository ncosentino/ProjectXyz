using System;
using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework.Events;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ICanBeSocketedBehavior : IBehavior
    {
        event EventHandler<EventArgs<Tuple<ICanBeSocketedBehavior, ICanFitSocketBehavior>>> Socketed;

        IReadOnlyCollection<ICanFitSocketBehavior> OccupiedSockets { get; }

        bool CanFitSocket(ICanFitSocketBehavior canFitSocket);

        bool Socket(ICanFitSocketBehavior canFitSocket);
    }
}