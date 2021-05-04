using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasDisplayNameBehavior : IBehavior
    {
        string DisplayName { get; }
    }
}
