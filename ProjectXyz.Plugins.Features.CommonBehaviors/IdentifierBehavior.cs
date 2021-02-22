using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class IdentifierBehavior :
        BaseBehavior,
        IIdentifierBehavior
    {
        public IdentifierBehavior()
            : this(null)
        {
        }

        public IdentifierBehavior(IIdentifier id)
        {
            Id = id;
        }

        public IIdentifier Id { get; set; }
    }
}
