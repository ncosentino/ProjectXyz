using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentExpressionInterceptor
    {
        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}