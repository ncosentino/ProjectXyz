using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Api.Behaviors
{
    public interface IHasEnchantments : IBehavior
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}