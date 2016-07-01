using System;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatExpressionInterceptor : IStatExpressionInterceptor
    {
        private readonly Func<IIdentifier, string, string> _interceptCallback;

        public StatExpressionInterceptor(Func<IIdentifier, string, string> interceptCallback)
        {
            _interceptCallback = interceptCallback;
        }

        public static IStatExpressionInterceptor Bypass { get; } = new StatExpressionInterceptor((_, x) => x);

        public string Intercept(IIdentifier statDefinitionId, string expression)
        {
            return _interceptCallback(statDefinitionId, expression);
        }
    }
}