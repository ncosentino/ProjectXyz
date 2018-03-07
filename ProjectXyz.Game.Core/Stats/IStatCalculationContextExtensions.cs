using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Framework.Entities.Interface;
using ProjectXyz.Game.Interface.Stats;

namespace ProjectXyz.Game.Core.Stats
{
    public static class IStatCalculationContextExtensions
    {
        public static IStatCalculationContext WithoutBaseStatEnchantments(this IStatCalculationContext statCalculationContext)
        {
            var newStatCalculationContext = new StatCalculationContext(
                statCalculationContext.Components,
                statCalculationContext.Enchantments.Where(x => !IEntityExtensions.Has<IAppliesToBaseStat>(x)));
            return newStatCalculationContext;
        }
    }
}