using System;

using ProjectXyz.Api.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased.Duration
{
    public interface IDurationTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IDurationTriggerBehavior durationTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback);
    }
}