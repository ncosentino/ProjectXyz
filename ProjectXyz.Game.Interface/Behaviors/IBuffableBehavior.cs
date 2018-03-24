using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IBuffableBehavior : IBehavior
    {
        void AddEnchantments(IEnumerable<IEnchantment> enchantments);
    }
}