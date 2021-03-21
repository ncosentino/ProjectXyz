﻿using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.States;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class ContextToExpressionInterceptorConverter : IContextToExpressionInterceptorConverter
    {
        private readonly IStateExpressionInterceptorFactory _stateExpressionInterceptorFactory;

        public ContextToExpressionInterceptorConverter(IStateExpressionInterceptorFactory stateExpressionInterceptorFactory)
        {
            _stateExpressionInterceptorFactory = stateExpressionInterceptorFactory;
        }

        public IEnchantmentExpressionInterceptor Convert(IEnchantmentCalculatorContext enchantmentCalculatorContext)
        {
            var stateContextProvider = enchantmentCalculatorContext
                .GetFirst<IComponent<IStateContextProvider>>()
                .Value;
            var stateEnchantmentExpressionInterceptor = _stateExpressionInterceptorFactory.Create(
                stateContextProvider,
                enchantmentCalculatorContext.Enchantments);
            return stateEnchantmentExpressionInterceptor;
        }
    }
}
