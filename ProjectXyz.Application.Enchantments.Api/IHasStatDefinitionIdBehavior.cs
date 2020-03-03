using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Enchantments
{
    public interface IHasStatDefinitionIdBehavior : IBehavior
    {
        IIdentifier StatDefinitionId { get; }
    }
}