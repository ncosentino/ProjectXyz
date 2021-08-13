using System;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments
{
    public interface IObservableHasEnchantmentsBehavior : IReadOnlyHasEnchantmentsBehavior
    {
        event EventHandler<EnchantmentsChangedEventArgs> EnchantmentsChanged;
    }
}