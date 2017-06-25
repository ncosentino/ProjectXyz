using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Game.Interface.Enchantments
{
    public interface IEnchantmentProvider
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}