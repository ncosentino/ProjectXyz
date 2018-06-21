using System.Collections.Generic;

namespace ProjectXyz.Api.Enchantments
{
    public interface IEnchantmentProvider
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}