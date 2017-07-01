using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm
{
    public interface IStatToTermExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IEnchantment> enchantments);
    }
}