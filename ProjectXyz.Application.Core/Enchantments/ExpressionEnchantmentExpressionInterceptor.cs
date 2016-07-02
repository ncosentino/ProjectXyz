using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class ExpressionEnchantmentExpressionInterceptor : IEnchantmentExpressionInterceptor
    {
        private readonly IStateContextProvider _stateContextProvider;
        private readonly IStateValueInjector _stateValueInjector;
        private readonly IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IExpressionEnchantment>> _statDefinitionToEnchantmentMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;

        public ExpressionEnchantmentExpressionInterceptor(
            IStateValueInjector stateValueInjector,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            IReadOnlyDictionary<IIdentifier, IReadOnlyCollection<IExpressionEnchantment>> statDefinitionToEnchantmentMapping,
            IStateContextProvider stateContextProvider)
        {
            _stateValueInjector = stateValueInjector;
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionToEnchantmentMapping = statDefinitionToEnchantmentMapping;
            _stateContextProvider = stateContextProvider;
        }

        public string Intercept(
            IIdentifier statDefinitionId,
            string expression)
        {
            var applicableEnchantments = _statDefinitionToEnchantmentMapping.GetValueOrDefault(
                statDefinitionId,
                () => new IExpressionEnchantment[0]);

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