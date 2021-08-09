using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Calculations
{
    public interface IStatExpressionInterceptor
    {
        int Priority { get; }

        string Intercept(
            IIdentifier statDefinitionId,
            string expression);
    }
}