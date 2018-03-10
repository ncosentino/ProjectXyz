using System;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Systems.ElapsedTime
{
    public sealed class ElapsedTimeComponentCreator : ISystemUpdateComponentCreator
    {
        private DateTime? _lastUpdateTime;

        public IComponent CreateNext()
        {
            var elapsedMilliseconds = _lastUpdateTime.HasValue
                ? (DateTime.UtcNow - _lastUpdateTime.Value).TotalMilliseconds
                : 0;
            _lastUpdateTime = DateTime.UtcNow;
            var elapsedInterval = new Interval<double>(elapsedMilliseconds);

            return new GenericComponent<IElapsedTime>(new ElapsedTime(elapsedInterval));
        }
    }
}
