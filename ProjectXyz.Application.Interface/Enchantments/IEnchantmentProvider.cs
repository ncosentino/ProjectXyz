using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentProvider
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}