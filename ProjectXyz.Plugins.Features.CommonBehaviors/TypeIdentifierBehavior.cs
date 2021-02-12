using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class TypeIdentifierBehavior :
       BaseBehavior,
       ITypeIdentifierBehavior
    {
        public IIdentifier TypeId { get; set; }
    }
}
