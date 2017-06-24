using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Enchantments.Api.Calculations
{
    public interface IEnchantmentExpressionInterceptor
    {
        int Priority { get; }

        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}