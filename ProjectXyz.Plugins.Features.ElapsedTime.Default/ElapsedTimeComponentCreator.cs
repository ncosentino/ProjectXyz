using System;
using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Default
{
    public sealed class ElapsedTimeComponentCreator : IDiscoverableSystemUpdateComponentCreator
    {
        private readonly IRealTimeProvider _realTimeProvider;
        private DateTime? _lastUpdateTime;

        public ElapsedTimeComponentCreator(IRealTimeProvider realTimeProvider)
        {
            _realTimeProvider = realTimeProvider;
        }

        public int? Priority => int.MinValue;

        public IEnumerable<IComponent> CreateNext(IReadOnlyCollection<IComponent> components)
        {
            var utcNow = _realTimeProvider.GetTimeUtc();
            var validTime =
                _lastUpdateTime.HasValue &&
                _lastUpdateTime != DateTime.MinValue &&
                _lastUpdateTime < utcNow;
            var elapsedMilliseconds = validTime
                ? (utcNow - _lastUpdateTime.Value).TotalMilliseconds
                : 0;
            _lastUpdateTime = utcNow;
            var elapsedInterval = new Interval<double>(elapsedMilliseconds);

            var elapsedTime = new ElapsedTime(elapsedInterval);
            var component = new GenericComponent<IElapsedTime>(elapsedTime);
            yield return component;
        }
    }
}
