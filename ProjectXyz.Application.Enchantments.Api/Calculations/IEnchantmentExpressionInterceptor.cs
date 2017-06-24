using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Api.Calculations
{
    public interface IEnchantmentExpressionInterceptor
    {
        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}