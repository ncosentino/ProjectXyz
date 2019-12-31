using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Stats.Calculations
{
    public interface IStatExpressionInterceptor
    {
        int Priority { get; }

        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}