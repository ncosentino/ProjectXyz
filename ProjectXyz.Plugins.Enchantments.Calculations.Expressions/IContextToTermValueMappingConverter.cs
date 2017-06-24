using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public interface IContextToTermValueMappingConverter : IConvert<IEnchantmentCalculatorContext, IReadOnlyDictionary<string, double>>
    {
    }
}