using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.TurnBased.Api.Duration
{
    public interface IDurationTriggerBehavior : IBehavior
    {
        double DurationInTurns { get; }
    }
}