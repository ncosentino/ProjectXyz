using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Enchantments
{
    public interface IEnchantment : IEntity
    {
        IIdentifier StatDefinitionId { get; }
    }
}