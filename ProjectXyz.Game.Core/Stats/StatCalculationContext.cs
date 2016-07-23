using System.Collections.Generic;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public sealed class StatCalculationContext : IStatCalculationContext
    {
        public StatCalculationContext(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            StateContextProvider = stateContextProvider;
            Enchantments = enchantments;
        }

        public IStateContextProvider StateContextProvider { get; }

        public IReadOnlyCollection<IEnchantment> Enchantments { get; }
    }
}