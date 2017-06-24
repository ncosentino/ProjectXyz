using System.Collections.Generic;

namespace ProjectXyz.Application.Enchantments.Api.Calculations
{
    public delegate KeyValuePair<string, double> ValueMapperDelegate(IEnchantmentCalculatorContext enchantmentCalculatorContext);
}