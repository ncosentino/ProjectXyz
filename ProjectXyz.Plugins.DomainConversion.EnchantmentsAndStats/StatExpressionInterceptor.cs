using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Plugins.DomainConversion.EnchantmentsAndStats
{
    public sealed class StatExpressionInterceptor : IStatExpressionInterceptor
    {
        private readonly InterceptDelegate _interceptCallback;

        public StatExpressionInterceptor(
            InterceptDelegate interceptCallback,
            int priority)
        {
            _interceptCallback = interceptCallback;
            Priority = priority;
        }

        public int Priority { get; }

        public string Intercept(IIdentifier statDefinitionId, string expression)
        {
            return _interceptCallback(statDefinitionId, expression);
        }
    }
}