using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantment : IEntity
    {
        IIdentifier StatDefinitionId { get; }
    }
}