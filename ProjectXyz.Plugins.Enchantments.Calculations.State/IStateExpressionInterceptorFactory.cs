using System.Collections.Generic;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Api;
using ProjectXyz.Application.Enchantments.Api.Calculations;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public interface IStateExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}