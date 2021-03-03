using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default.Calculations
{
    public sealed class OverrideBaseStatComponent : IOverrideBaseStatComponent
    {
        public OverrideBaseStatComponent(
            IIdentifier statDefinitionId,
            double value,
            int priority)
        {
            StatDefinitionId = statDefinitionId;
            Value = value;
            Priority = priority;
        }

        public IIdentifier StatDefinitionId { get; }

        public double Value { get; }

        public int Priority { get; }
    }
}