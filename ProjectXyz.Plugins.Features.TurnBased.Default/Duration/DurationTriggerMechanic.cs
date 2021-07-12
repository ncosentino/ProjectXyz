using System;
using System.Linq;

using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
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