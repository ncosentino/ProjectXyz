using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentCalculatorContextComponentHandler
    {
        bool CanHandle(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId,
            IReadOnlyCollection<IComponent> components);

        IEnumerable<KeyValuePair<IIdentifier, double>> OverrideBaseStat(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId,
            IReadOnlyCollection<IComponent> components);
    }
}