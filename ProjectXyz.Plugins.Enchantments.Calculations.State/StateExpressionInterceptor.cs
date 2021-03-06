﻿using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.States;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class StateExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IStateContextProvider _stateContextProvider;
        private readonly IStateValueInjector _stateValueInjector;

        public StateExpressionInterceptor(
            IStateValueInjector stateValueInjector,
            IStateContextProvider stateContextProvider,
            int priority)
        {
            _stateValueInjector = stateValueInjector;
            _stateContextProvider = stateContextProvider;
            Priority = priority;
        }

        public int Priority { get; }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            expression = _stateValueInjector.Inject(
                _stateContextProvider,
                expression);
            return expression;
        }
    }
}