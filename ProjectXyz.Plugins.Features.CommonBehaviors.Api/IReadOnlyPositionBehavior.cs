
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IReadOnlyPositionBehavior : IBehavior
    {
        double X { get; }

        double Y { get; }
    }
}