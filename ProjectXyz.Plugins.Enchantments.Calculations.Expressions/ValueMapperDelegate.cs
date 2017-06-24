using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public delegate KeyValuePair<string, double> ValueMapperDelegate(IEnchantmentCalculatorContext enchantmentCalculatorContext);
}