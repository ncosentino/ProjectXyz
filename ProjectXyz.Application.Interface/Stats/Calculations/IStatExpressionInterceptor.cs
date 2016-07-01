using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Interface.Stats.Calculations
{
    public interface IStatExpressionInterceptor
    {
        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}