
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Entities;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IOverrideBaseStatComponent : IComponent
    {
        int Priority { get; }

        IIdentifier StatDefinitionId { get; }

        double Value { get; }
    }
}