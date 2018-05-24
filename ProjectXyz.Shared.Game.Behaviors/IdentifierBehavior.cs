using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Game.Behaviors
{
    public sealed class IdentifierBehavior : 
        BaseBehavior,
        IIdentifierBehavior
    {
        public IIdentifier Id { get; set; }
    }
}
