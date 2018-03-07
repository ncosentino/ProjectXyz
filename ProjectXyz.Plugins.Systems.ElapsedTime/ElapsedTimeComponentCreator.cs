using System;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Entities.Shared;
using ProjectXyz.Framework.Shared;
using ProjectXyz.Game.Interface.Systems;

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

            return new GenericComponent<IElapsedTime>(new Game.Core.Systems.ElapsedTime(elapsedInterval));
        }
    }
}
