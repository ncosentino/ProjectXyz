using System;

using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased.Duration
{
    public interface IDurationInActionsTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IDurationInActionsTriggerBehavior durationTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback);
    }
}