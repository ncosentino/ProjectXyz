using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IReadOnlyPlayerControlledBehavior : IBehavior
    {
        bool IsActive { get; }
    }
}
