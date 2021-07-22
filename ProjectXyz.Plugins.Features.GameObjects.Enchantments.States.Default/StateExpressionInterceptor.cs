using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default
{
    public sealed class StateExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IStateValueInjector _stateValueInjector;

        public StateExpressionInterceptor(
            IStateValueInjector stateValueInjector,
            int priority)
        {
            _stateValueInjector = stateValueInjector;
            Priority = priority;
        }

        public int Priority { get; }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            expression = _stateValueInjector.Inject(expression);
            return expression;
        }
    }
}