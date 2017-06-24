using System.Collections.Generic;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.Enchantments.Calculations.Expressions
{
    public interface IContextToTermValueMappingConverter : IConvert<IEnchantmentCalculatorContext, IReadOnlyDictionary<string, double>>
    {
    }
}