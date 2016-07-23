using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatCalculationContext
    {
        IStateContextProvider StateContextProvider { get; }

        IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}