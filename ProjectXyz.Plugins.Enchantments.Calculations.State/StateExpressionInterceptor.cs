﻿using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments.Calculations;
using ProjectXyz.Api.States;
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
            IStateContextProvider stateContextProvider,
            int priority)
        {
            _stateValueInjector = stateValueInjector;
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionToComponentMapping = statDefinitionToComponentMapping;
            _stateContextProvider = stateContextProvider;
            Priority = priority;
        }

        public int Priority { get; }

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