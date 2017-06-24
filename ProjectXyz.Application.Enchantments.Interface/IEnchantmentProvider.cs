using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api;

namespace ProjectXyz.Application.Enchantments.Interface
{
    public interface IEnchantmentProvider
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}