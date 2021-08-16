using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IReadOnlyPositionBehavior : IBehavior
    {
        double X { get; }

        double Y { get; }
    }
}