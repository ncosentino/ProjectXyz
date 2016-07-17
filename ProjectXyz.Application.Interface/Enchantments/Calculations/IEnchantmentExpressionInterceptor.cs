using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments.Calculations
{
    public interface IEnchantmentExpressionInterceptor
    {
        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}