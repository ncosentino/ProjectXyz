using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Api;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class StateExpressionInterceptorFactory : IStateExpressionInterceptorFactory
    {
        private readonly int _priority;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;
        private readonly IStateValueInjector _stateValueInjector;

        public StateExpressionInterceptorFactory(
            IStateValueInjector stateValueInjector,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            int priority)
        {
            _stateValueInjector = stateValueInjector;
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _priority = priority;
        }

        public IEnchantmentExpressionInterceptor Create(
            IStateContextProvider stateContextProvider,
            IReadOnlyCollection<IEnchantment> enchantments)
        {
            var statDefinitionToComponentMapping = enchantments
                .GroupBy(
                    enchantment => enchantment.StatDefinitionId,
                    enchantment => enchantment)
                .ToDictionary(
                    group => group.Key,
                    group => group
                        .SelectMany(enchantment => enchantment.Components.Get<IEnchantmentExpressionComponent>())
                        .OrderBy(component => component.CalculationPriority)
                        .ToReadOnlyCollection());

            var interceptor = new StateExpressionInterceptor(
                _stateValueInjector,
                _statDefinitionIdToTermMapping,
                statDefinitionToComponentMapping,
                stateContextProvider,
                _priority);
            return interceptor;
        }
    }
}