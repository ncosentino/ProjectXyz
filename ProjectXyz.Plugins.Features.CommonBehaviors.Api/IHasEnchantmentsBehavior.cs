using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasEnchantmentsBehavior : IHasReadOnlyEnchantmentsBehavior
    {
        void AddEnchantments(IEnumerable<IGameObject> enchantments);

        void RemoveEnchantments(IEnumerable<IGameObject> enchantments);
    }
}