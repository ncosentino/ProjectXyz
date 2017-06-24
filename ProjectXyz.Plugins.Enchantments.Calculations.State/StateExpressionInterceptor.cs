using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.States;
using ProjectXyz.Application.Enchantments.Api.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.Calculations.State
{
    public sealed class StateExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IStateContextProvider _stateContextProvider;
        private readonly IStateValueInjector _stateValueInjector;
        private readonly IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IEnchantmentExpressionComponent>> _statDefinitionToComponentMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public StateExpressionInterceptor(
            IStateValueInjector stateValueInjector,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IEnchantmentExpressionComponent>> statDefinitionToComponentMapping,
            IStateContextProvider stateContextProvider)
        {
            _stateValueInjector = stateValueInjector;
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionToComponentMapping = statDefinitionToComponentMapping;
            _stateContextProvider = stateContextProvider;
        }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            var applicableEnchantments = _statDefinitionToComponentMapping.GetValueOrDefault(
                statDefinitionId,
                () => new IEnchantmentExpressionComponent[0]);

            var term = _statDefinitionIdToTermMapping[statDefinitionId];

            expression = applicableEnchantments.Aggregate(
                expression,
                (current, enchantment) => enchantment.Expression.Replace(
                    term,
                    $"({current})"));
            expression = _stateValueInjector.Inject(
                _stateContextProvider,
                expression);
            return expression;
        }
    }
}