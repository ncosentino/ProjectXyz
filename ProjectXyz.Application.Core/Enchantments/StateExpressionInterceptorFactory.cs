using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class StateExpressionInterceptorFactory : IStateExpressionInterceptorFactory
    {
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;
        private readonly IStateValueInjector _stateValueInjector;

        public StateExpressionInterceptorFactory(
            IStateValueInjector stateValueInjector,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping)
        {
            _stateValueInjector = stateValueInjector;
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
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
                stateContextProvider);
            return interceptor;
        }
    }
}