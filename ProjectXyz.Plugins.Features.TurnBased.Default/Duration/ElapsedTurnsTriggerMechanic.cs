using System;
using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class ElapsedTurnsTriggerMechanic : IElapsedTurnsTriggerMechanic
    {
        private readonly Action<IElapsedTurnsTriggerMechanic, IDurationInTurnsTriggerBehavior> _triggerCallback;
        private readonly double _target;
        private readonly IDurationInTurnsTriggerBehavior _durationTriggerBehavior;

        private double _elapsedTurns;

        public ElapsedTurnsTriggerMechanic(
            IDurationInTurnsTriggerBehavior durationTriggerBehavior,
            Action<IElapsedTurnsTriggerMechanic, IDurationInTurnsTriggerBehavior> triggerCallback)
        {
            _triggerCallback = triggerCallback;
            _durationTriggerBehavior = durationTriggerBehavior;
            _target = durationTriggerBehavior.DurationInTurns;
        }

        public async Task<bool> UpdateAsync(ITurnInfo turnInfo)
        {
            if (turnInfo.AllGameObjects.Contains(_durationTriggerBehavior.Owner))
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