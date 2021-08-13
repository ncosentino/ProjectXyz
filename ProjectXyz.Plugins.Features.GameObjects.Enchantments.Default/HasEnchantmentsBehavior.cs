using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    public sealed class HasEnchantmentsBehavior :
        BaseBehavior,
        IHasEnchantmentsBehavior
    {
        private readonly IActiveEnchantmentManager _activeEnchantmentManager;

        public HasEnchantmentsBehavior(IActiveEnchantmentManager activeEnchantmentManager)
        {
            _activeEnchantmentManager = activeEnchantmentManager;
        }

        public event EventHandler<EnchantmentsChangedEventArgs> EnchantmentsChanged;

        public IReadOnlyCollection<IGameObject> Enchantments => _activeEnchantmentManager.Enchantments;

        public async Task AddEnchantmentsAsync(IEnumerable<IGameObject> enchantments)
        {
            var added = new List<IGameObject>();
            foreach (var enchantment in enchantments)
            {
                _activeEnchantmentManager.Add(enchantment);
                added.Add(enchantment);
            }

            if (added.Any())
            {
                await EnchantmentsChanged
                    .InvokeOrderedAsync(
                        this,
                        new EnchantmentsChangedEventArgs(
                            added,
                            Enumerable.Empty<IGameObject>()))
                    .ConfigureAwait(false);
            }
        }

        public async Task RemoveEnchantmentsAsync(IEnumerable<IGameObject> enchantments)
        {
            var removed = new List<IGameObject>();
            foreach (var enchantment in enchantments)
            {
                _activeEnchantmentManager.Remove(enchantment);
                removed.Add(enchantment);
            }

            if (removed.Any())
            {
                await EnchantmentsChanged
                    .InvokeOrderedAsync(
                        this,
                        new EnchantmentsChangedEventArgs(
                            Enumerable.Empty<IGameObject>(),
                            removed))
                    .ConfigureAwait(false);
            }
        }
    }
}
