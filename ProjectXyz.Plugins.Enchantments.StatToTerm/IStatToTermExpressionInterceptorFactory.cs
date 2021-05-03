using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Enchantments.StatToTerm
{
    public interface IStatToTermExpressionInterceptorFactory
    {
        IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IGameObject> enchantments);
    }
}