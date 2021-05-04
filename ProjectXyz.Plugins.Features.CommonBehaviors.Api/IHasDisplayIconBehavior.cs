using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasDisplayIconBehavior : IBehavior
    {
        IIdentifier IconResourceId { get; }
    }
}
