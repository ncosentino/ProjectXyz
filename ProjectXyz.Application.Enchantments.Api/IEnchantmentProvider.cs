using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments
{
    public interface IEnchantmentProvider
    {
        IReadOnlyCollection<IGameObject> Enchantments { get; }
    }
}