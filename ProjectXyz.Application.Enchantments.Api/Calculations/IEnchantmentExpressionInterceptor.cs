using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Api.Enchantments.Calculations
{
    public interface IEnchantmentExpressionInterceptor
    {
        int Priority { get; }

        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}