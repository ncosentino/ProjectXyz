using System.Collections.Generic;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Api;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public interface IStateExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}