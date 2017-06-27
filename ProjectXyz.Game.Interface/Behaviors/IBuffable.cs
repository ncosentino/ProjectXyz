using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IBuffable : IBehavior
    {
        void AddEnchantments(IEnumerable<IEnchantment> enchantments);
    }
}