using ProjectXyz.Plugins.Features.TurnBased.Duration;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class DurationTriggerBehavior :
        BaseBehavior,
        IDurationTriggerBehavior
    {
        public DurationTriggerBehavior(double durationInTurns)
        {
            DurationInTurns = durationInTurns;
        }

        public double DurationInTurns { get; }
    }
}