using System.Collections.Generic;

using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States.Default
{
    public sealed class StateExpressionInterceptorFactory : IStateExpressionInterceptorFactory
    {
        private readonly int _priority;
        private readonly IStateValueInjector _stateValueInjector;

        public StateExpressionInterceptorFactory(
            IStateValueInjector stateValueInjector,
            int priority)
        {
            _stateValueInjector = stateValueInjector;
            _priority = priority;
        }

        public IEnchantmentExpressionInterceptor Create(IReadOnlyCollection<IGameObject> enchantments)
        {
            var interceptor = new StateExpressionInterceptor(
                _stateValueInjector,
                _priority);
            return interceptor;
        }
    }
}