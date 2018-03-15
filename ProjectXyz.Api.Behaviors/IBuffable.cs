using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Api.Behaviors
{
    public interface IBuffable : IBehavior
    {
        void AddEnchantments(IEnumerable<IEnchantment> enchantments);
    }
}