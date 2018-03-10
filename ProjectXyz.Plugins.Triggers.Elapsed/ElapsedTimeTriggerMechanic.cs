using System;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Triggering;
using ProjectXyz.Api.Triggering.Elapsed;

namespace ProjectXyz.Plugins.Triggers.Elapsed
{
    public sealed class ElapsedTimeTriggerMechanic : IElapsedTimeTriggerMechanic
    {
        private readonly Action<IElapsedTimeTriggerMechanic, IElapsedTimeTriggerComponent> _triggerCallback;
        
        public ElapsedTimeTriggerMechanic(Action<IElapsedTimeTriggerMechanic, IElapsedTimeTriggerComponent> triggerCallback)
        {
            _triggerCallback = triggerCallback;
        }

        public bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar)
        {
            return triggerMechanicRegistrar is IElapsedTimeTriggerSourceMechanic;
        }

        public bool Update(IInterval elapsed)
        {
            _triggerCallback(this, new ElapsedTimeTriggerComponent(elapsed));
            return false;
        }
    }
}