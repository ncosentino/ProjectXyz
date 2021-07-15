using System;
using System.Threading.Tasks;

using ProjectXyz.Plugins.Features.TurnBased.Duration;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class ElapsedActionsTriggerMechanic : IElapsedActionsTriggerMechanic
    {
        private readonly Action<IElapsedActionsTriggerMechanic, IDurationInActionsTriggerBehavior> _triggerCallback;
        private readonly double _target;
        private readonly IDurationInActionsTriggerBehavior _durationTriggerBehavior;

        private double _elapsedActions;

        public ElapsedActionsTriggerMechanic(
            IDurationInActionsTriggerBehavior durationTriggerBehavior,
            Action<IElapsedActionsTriggerMechanic, IDurationInActionsTriggerBehavior> triggerCallback)
        {
            _triggerCallback = triggerCallback;
            _durationTriggerBehavior = durationTriggerBehavior;
            _target = durationTriggerBehavior.DurationInActions;
        }

        public async Task<bool> UpdateAsync(IActionInfo actionInfo)
        {
            if (actionInfo.AllGameObjects.Contains(_durationTriggerBehavior.Owner))
            {
                return true;
            }

            _elapsedActions += actionInfo.ElapsedActions;

            if (_elapsedActions < _target)
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