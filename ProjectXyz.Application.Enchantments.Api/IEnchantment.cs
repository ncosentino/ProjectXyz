using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments
{
    public interface IEnchantment : IGameObject
    {
        IIdentifier StatDefinitionId { get; }
    }
}