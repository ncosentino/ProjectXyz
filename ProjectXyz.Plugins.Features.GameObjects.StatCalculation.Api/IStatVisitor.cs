using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public interface IStatVisitor
    {
        double GetStatValue(
            IGameObject gameObject,
            IIdentifier statId,
            IStatCalculationContext context);
    }
}
