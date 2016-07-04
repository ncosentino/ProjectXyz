using System.Collections.Generic;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentApplier
    {
        IReadOnlyDictionary<IIdentifier, IStat> ApplyEnchantments(
            IStateContextProvider stateContextProvider,
            IReadOnlyDictionary<IIdentifier, IStat> baseStats,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}