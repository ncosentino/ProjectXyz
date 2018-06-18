using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.ExpressionEnchantments
{
    public interface IContextToTermValueMappingConverter : IConvert<IEnchantmentCalculatorContext, IReadOnlyDictionary<string, double>>
    {
    }
}