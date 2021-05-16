using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IReadOnlySizeBehavior : IBehavior
    {
        double Width { get; }

        double Height { get; }
    }
}