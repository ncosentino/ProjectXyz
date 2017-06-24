using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Api
{
    public interface IEnchantment : IEntity
    {
        IIdentifier StatDefinitionId { get; }
    }
}