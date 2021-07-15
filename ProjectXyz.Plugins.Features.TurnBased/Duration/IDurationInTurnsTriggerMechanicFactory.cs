using System;

using ProjectXyz.Plugins.Features.Triggering;

namespace ProjectXyz.Plugins.Features.TurnBased.Duration
{
    public interface IDurationInTurnsTriggerMechanicFactory
    {
        ITriggerMechanic Create(
            IDurationInTurnsTriggerBehavior durationTriggerBehavior,
            Action<ITriggerMechanic> triggeredCallback);
    }
}