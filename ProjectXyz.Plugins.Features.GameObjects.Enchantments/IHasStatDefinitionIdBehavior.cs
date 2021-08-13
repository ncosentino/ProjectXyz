using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.Enchantments
{
    public interface IHasStatDefinitionIdBehavior : IBehavior
    {
        IIdentifier StatDefinitionId { get; }
    }
}