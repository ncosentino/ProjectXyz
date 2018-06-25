using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.ElapsedTime.Api;
using ProjectXyz.Plugins.Features.ElapsedTime.Api.Duration;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Duration
{
    public sealed class DurationTriggerMechanic : IElapsedTimeTriggerMechanic
    {
        private readonly Action<IElapsedTimeTriggerMechanic, IDurationTriggerBehavior> _triggerCallback;
        private readonly IInterval _target;
        private readonly IDurationTriggerBehavior _elapsedTimeExpiryTriggerBehavior;

        private IInterval _elapsed;

        public DurationTriggerMechanic(
            IDurationTriggerBehavior elapsedTimeExpiryTriggerBehavior,
            Action<IElapsedTimeTriggerMechanic, IDurationTriggerBehavior> triggerCallback)
        {
            _triggerCallback = triggerCallback;
            _elapsedTimeExpiryTriggerBehavior = elapsedTimeExpiryTriggerBehavior;
            _target = elapsedTimeExpiryTriggerBehavior.Duration;
        }

        public bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar)
        {
            return triggerMechanicRegistrar is IElapsedTimeTriggerSourceMechanic;
        }

        public bool Update(IInterval elapsed)
        {
            _elapsed = _elapsed == null
                ? elapsed
                : _elapsed.Add(elapsed);

            if (_elapsed.CompareTo(_target) < 0)
            {
                return true;
            }

            _triggerCallback(
                this,
                _elapsedTimeExpiryTriggerBehavior);
            return false;
        }
    }
}