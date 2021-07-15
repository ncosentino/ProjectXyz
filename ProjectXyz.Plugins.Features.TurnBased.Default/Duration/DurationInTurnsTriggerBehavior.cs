using ProjectXyz.Plugins.Features.TurnBased.Duration;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.TurnBased.Default.Duration
{
    public sealed class DurationInTurnsTriggerBehavior :
        BaseBehavior,
        IDurationInTurnsTriggerBehavior
    {
        public DurationInTurnsTriggerBehavior(double durationInTurns)
        {
            DurationInTurns = durationInTurns;
        }

        public double DurationInTurns { get; }
    }
}