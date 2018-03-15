using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments.Api
{
    public delegate KeyValuePair<string, double> ValueMapperDelegate(IEnchantmentCalculatorContext enchantmentCalculatorContext);
}