using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IStateEnchantmentExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}