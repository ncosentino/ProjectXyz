using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments
{
    public interface IEnchantment : IEntity
    {
        IIdentifier StatDefinitionId { get; }
    }
}