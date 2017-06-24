using System.Collections.Generic;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public delegate KeyValuePair<string, double> ValueMapperDelegate(IEnchantmentCalculatorContext enchantmentCalculatorContext);
}