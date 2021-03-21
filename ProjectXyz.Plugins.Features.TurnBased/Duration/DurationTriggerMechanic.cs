using System;
using System.Linq;

using ProjectXyz.Api.Triggering;
using ProjectXyz.Plugins.Features.TurnBased.Api;
using ProjectXyz.Plugins.Features.TurnBased.Api.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Duration
{
    public sealed class DurationTriggerMechanic : IElapsedTurnsTriggerMechanic
    {
        private readonly Action<IElapsedTurnsTriggerMechanic, IDurationTriggerBehavior> _triggerCallback;
        private readonly double _target;
        private readonly IDurationTriggerBehavior _durationTriggerBehavior;

        private double _elapsedTurns;

        public DurationTriggerMechanic(
            IDurationTriggerBehavior durationTriggerBehavior,
            Action<IElapsedTurnsTriggerMechanic, IDurationTriggerBehavior> triggerCallback)
        {
            _triggerCallback = triggerCallback;
            _durationTriggerBehavior = durationTriggerBehavior;
            _target = durationTriggerBehavior.DurationInTurns;
        }

        public bool CanBeRegisteredTo(ITriggerMechanicRegistrar triggerMechanicRegistrar)
        {
            return triggerMechanicRegistrar is IElapsedTurnsTriggerSourceMechanic;
        }

        public bool Update(ITurnInfo turnInfo)
        {
            if (!turnInfo.GlobalSync && 
                turnInfo.ApplicableGameObjects.Contains(_durationTriggerBehavior.Owner))
            {
                return true;
            }

            _elapsedTurns += turnInfo.ElapsedTurns;

            if (_elapsedTurns < _target)
            {
                return true;
            }

            _triggerCallback(
                this,
                _durationTriggerBehavior);
            return false;
        }
    }
}