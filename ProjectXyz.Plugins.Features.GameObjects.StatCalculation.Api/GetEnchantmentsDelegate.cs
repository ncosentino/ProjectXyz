using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api
{
    public delegate IReadOnlyCollection<IEnchantment> GetEnchantmentsDelegate(
        IStatVisitor statVisitor,
        IEnchantmentVisitor enchantmentVisitor,
        IHasBehaviors behaviors,
        IIdentifier target,
        IIdentifier statId,
        IStatCalculationContext context);
}
