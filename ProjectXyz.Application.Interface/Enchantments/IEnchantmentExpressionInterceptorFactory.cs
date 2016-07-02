using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}