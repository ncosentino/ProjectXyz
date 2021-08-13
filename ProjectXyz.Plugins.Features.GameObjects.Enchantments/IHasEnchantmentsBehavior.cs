using System.Collections.Generic;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments
{
    public interface IHasEnchantmentsBehavior : IObservableHasEnchantmentsBehavior
    {
        Task AddEnchantmentsAsync(IEnumerable<IGameObject> enchantments);

        Task RemoveEnchantmentsAsync(IEnumerable<IGameObject> enchantments);
    }
}