using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
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

        public IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            var interceptor = new StateExpressionInterceptor(
                _stateValueInjector,
                stateContextProvider,
                _priority);
            return interceptor;
        }
    }
}