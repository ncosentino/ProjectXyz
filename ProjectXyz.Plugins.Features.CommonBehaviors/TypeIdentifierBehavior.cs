using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class TypeIdentifierBehavior :
       BaseBehavior,
       ITypeIdentifierBehavior
    {
        public TypeIdentifierBehavior()
            : this(null)
        {
        }

        public TypeIdentifierBehavior(IIdentifier typeId)
        {
            TypeId = typeId;
        }

        public IIdentifier TypeId { get; set; }
    }
}
