using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ITargetPatternBehavior : IBehavior
    {
        IReadOnlyCollection<Tuple<int, int>> LocationsOffsetFromOrigin { get; }
    }

    public sealed class TargetPatternBehavior : BaseBehavior, ITargetPatternBehavior
    {
        public TargetPatternBehavior(
             IEnumerable<Tuple<int, int>> locationsOffsetFromOrigin)
        {
            LocationsOffsetFromOrigin = locationsOffsetFromOrigin.ToArray();
        }

        public IReadOnlyCollection<Tuple<int, int>> LocationsOffsetFromOrigin { get; }
    }
}
