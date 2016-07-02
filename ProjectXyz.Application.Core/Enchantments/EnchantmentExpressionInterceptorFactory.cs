using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Enchantments
{
    public sealed class EnchantmentExpressionInterceptorFactory : IEnchantmentExpressionInterceptorFactory
    {
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;
        private readonly IStateValueInjector _stateValueInjector;

        public EnchantmentExpressionInterceptorFactory(
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
            var statDefinitionToEnchantmentMapping = enchantments
                .TakeTypes<IExpressionEnchantment>()
                .GroupBy(
                    x => x.StatDefinitionId,
                    x => x)
                .ToDictionary(
                    x => x.Key,
                    x => (IReadOnlyCollection<IExpressionEnchantment>)x.Select(g => g).OrderBy(e => e.CalculationPriority).ToArray());

            var interceptor = new ExpressionEnchantmentExpressionInterceptor(
                _stateValueInjector,
                _statDefinitionIdToTermMapping,
                statDefinitionToEnchantmentMapping,
                stateContextProvider);
            return interceptor;
        }
    }
}