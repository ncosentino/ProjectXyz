using ProjectXyz.Plugins.Features.TurnBased.Duration;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class DurationInActionsTriggerBehavior :
        BaseBehavior,
        IDurationInActionsTriggerBehavior
    {
        public DurationInActionsTriggerBehavior(double durationInActions)
        {
            DurationInActions = durationInActions;
        }

        public double DurationInActions { get; }
    }
}