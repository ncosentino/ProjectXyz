using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    public sealed class OverrideBaseStatComponentHandler : IEnchantmentCalculatorContextComponentHandler
    {
        public bool CanHandle(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId,
            IReadOnlyCollection<IComponent> components) =>
            components.TakeTypes<IOverrideBaseStatComponent>().Any();

        public IEnumerable<KeyValuePair<IIdentifier, double>> OverrideBaseStat(
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId,
            IReadOnlyCollection<IComponent> components)
        {
            foreach (var component in components
                .TakeTypes<IOverrideBaseStatComponent>()
                .OrderBy(x => x.Priority))
            {
                yield return KeyValuePair.Create(
                    component.StatDefinitionId,
                    component.Value);
            }
        }
    }
}