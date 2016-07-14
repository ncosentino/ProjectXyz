using System.Collections.Generic;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IStateExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments);
    }
}