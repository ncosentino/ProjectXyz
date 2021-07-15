using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.TurnBased.Duration
{
    public interface IDurationInActionsTriggerBehavior : IBehavior
    {
        double DurationInActions { get; }
    }
}