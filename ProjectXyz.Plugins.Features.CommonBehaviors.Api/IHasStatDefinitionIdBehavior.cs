using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasStatDefinitionIdBehavior : IBehavior
    {
        IIdentifier StatDefinitionId { get; }
    }
}