using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Triggers.Elapsed.Duration
{
    public sealed class DurationTriggerMechanic : IElapsedTimeTriggerMechanic
    {
        private readonly Action<IElapsedTimeTriggerMechanic, IDurationTriggerComponent> _triggerCallback;
        private readonly IInterval _target;
        private readonly IDurationTriggerComponent _elapsedTimeExpiryTriggerComponent;

        private IInterval _elapsed;

        public DurationTriggerMechanic(
            IDurationTriggerComponent elapsedTimeExpiryTriggerComponent,
            Action<IElapsedTimeTriggerMechanic, IDurationTriggerComponent> triggerCallback)
        {
            _triggerCallback = triggerCallback;
            _elapsedTimeExpiryTriggerComponent = elapsedTimeExpiryTriggerComponent;
            _target = elapsedTimeExpiryTriggerComponent.Duration;
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
                _elapsedTimeExpiryTriggerComponent);
            return false;
        }
    }
}