using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasDisplayIconBehavior : BaseBehavior, IHasDisplayIconBehavior
    {
        public HasDisplayIconBehavior(
            IIdentifier iconResourceId)
        {
            IconResourceId = iconResourceId;
        }

        public IIdentifier IconResourceId { get; }
    }
}
