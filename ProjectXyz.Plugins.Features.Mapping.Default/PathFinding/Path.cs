using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using ProjectXyz.Plugins.Features.Mapping;

namespace ProjectXyz.Plugins.Features.Mapping.Default.PathFinding
{
    public sealed class Path : IPath
    {
        private static readonly Lazy<IPath> LAZY_EMPTY = new Lazy<IPath>(() =>
            new Path(Enumerable.Empty<KeyValuePair<Vector2, double>>()));

        private readonly Lazy<double> _lazyTotalDistance;

        public Path(IEnumerable<KeyValuePair<Vector2, double>> accumulatedDistancePerPoint)
        {
            AccumulatedDistancePerPoint = accumulatedDistancePerPoint.ToDictionary(x => x.Key, x => x.Value);
            _lazyTotalDistance = new Lazy<double>(() => AccumulatedDistancePerPoint.Last().Value);
        }

        public static IPath Empty => LAZY_EMPTY.Value;

        public IReadOnlyCollection<Vector2> Positions => AccumulatedDistancePerPoint.Keys.ToArray();

        public IReadOnlyDictionary<Vector2, double> AccumulatedDistancePerPoint { get; }

        public double TotalDistance => _lazyTotalDistance.Value;
    }
}