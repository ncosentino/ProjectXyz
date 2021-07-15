using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.TurnBased.Duration
{
    public interface IDurationInTurnsTriggerBehavior : IBehavior
    {
        double DurationInTurns { get; }
    }
}