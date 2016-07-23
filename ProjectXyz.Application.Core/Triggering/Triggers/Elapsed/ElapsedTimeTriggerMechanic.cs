using System;
using ProjectXyz.Application.Interface.Triggering;
using ProjectXyz.Application.Interface.Triggering.Triggers.Elapsed;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Triggering.Triggers.Elapsed
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