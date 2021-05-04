using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasDisplayNameBehavior : BaseBehavior, IHasDisplayNameBehavior
    {
        public HasDisplayNameBehavior(
            string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }
}
