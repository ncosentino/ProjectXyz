﻿using System;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.Systems;
using ProjectXyz.Shared.Framework;
using ProjectXyz.Shared.Framework.Entities;

namespace ProjectXyz.Plugins.Features.ElapsedTime
{
    public sealed class ElapsedTimeComponentCreator : IDiscoverableSystemUpdateComponentCreator
    {
        private DateTime? _lastUpdateTime;

        public int? Priority => int.MinValue;

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
