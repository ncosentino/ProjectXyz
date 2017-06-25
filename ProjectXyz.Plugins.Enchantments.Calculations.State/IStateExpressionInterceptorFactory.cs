using System.Collections.Generic;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public interface IStateExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}