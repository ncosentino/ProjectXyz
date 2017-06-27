using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface IHasEnchantments : IBehavior
    {
        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}