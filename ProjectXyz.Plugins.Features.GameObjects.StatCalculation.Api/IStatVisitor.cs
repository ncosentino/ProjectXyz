using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface IStatVisitor
    {
        double GetStatValue(
            IHasBehaviors gameObject,
            IIdentifier statId,
            IStatCalculationContext context);
    }
}
