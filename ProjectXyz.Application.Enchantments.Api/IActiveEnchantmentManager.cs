using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Enchantments
{
    public interface IActiveEnchantmentManager : IEnchantmentProvider
    {
        void Add(IEnumerable<IGameObject> enchantment);

        void Remove(IEnumerable<IGameObject> enchantment);
    }
}