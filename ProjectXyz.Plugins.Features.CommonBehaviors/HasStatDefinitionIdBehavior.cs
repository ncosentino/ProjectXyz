using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
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
