using System.Collections.Generic;
using System.Numerics;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IPath
    {
        IReadOnlyCollection<Vector2> Positions { get; }

        IReadOnlyDictionary<Vector2, double> AccumulatedDistancePerPoint { get; }

        double TotalDistance { get; }
    }
}