using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Api.Behaviors
{
    public interface IHasEnchantmentsBehavior : IBehavior
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}