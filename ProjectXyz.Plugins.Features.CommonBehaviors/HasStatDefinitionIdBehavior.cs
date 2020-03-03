using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasStatDefinitionIdBehavior :
        BaseBehavior,
        IHasStatDefinitionIdBehavior
    {
        public IIdentifier StatDefinitionId { get; set; }
    }
}
