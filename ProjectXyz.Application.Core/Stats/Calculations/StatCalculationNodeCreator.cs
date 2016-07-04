using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatCalculationNodeCreator : IStatCalculationNodeCreator
    {
        private readonly IStatExpressionInterceptor _statBoundsExpressionInterceptor;
        private readonly IStatCalculationNodeFactory _statCalculationNodeFactory;
        private readonly IExpressionStatDefinitionDependencyFinder _expressionStatDefinitionDependencyFinder;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToCalculationMapping;

        public StatCalculationNodeCreator(
            IStatCalculationNodeFactory statCalculationNodeFactory,
            IExpressionStatDefinitionDependencyFinder expressionStatDefinitionDependencyFinder,
            IStatExpressionInterceptor statBoundsExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToCalculationMapping)
        {
            _statCalculationNodeFactory = statCalculationNodeFactory;
            _expressionStatDefinitionDependencyFinder = expressionStatDefinitionDependencyFinder;
            _statBoundsExpressionInterceptor = statBoundsExpressionInterceptor;
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionIdToCalculationMapping = statDefinitionIdToCalculationMapping;
        }

        public IStatCalculationNode Create(
            IStatExpressionInterceptor statExpressionInterceptor,
            IReadOnlyDictionary<IIdentifier, double> baseStats,
            IIdentifier statDefinitionId)
        {
            var cache = new Dictionary<IIdentifier, IStatCalculationNode>();
            var stack = new Stack<IIdentifier>();
            stack.Push(statDefinitionId);

            while (stack.Any())
            {
                var currentStatDefinitionId = stack.Pop();

                var expression = baseStats.ContainsKey(currentStatDefinitionId)
                    ? baseStats[currentStatDefinitionId].ToString(CultureInfo.InvariantCulture)
                    : _statDefinitionIdToCalculationMapping.ContainsKey(currentStatDefinitionId)
                        ? _statDefinitionIdToCalculationMapping[currentStatDefinitionId]
                        : "0";
                expression = statExpressionInterceptor.Intercept(
                    currentStatDefinitionId,
                    expression);
                expression = _statBoundsExpressionInterceptor.Intercept(
                    currentStatDefinitionId,
                    expression);

                var dependentStatDefinitionIds = _expressionStatDefinitionDependencyFinder.FindDependencies(
                    _statDefinitionIdToTermMapping,
                    expression);

                var canMakeNode =
                    !dependentStatDefinitionIds.Any() ||
                    dependentStatDefinitionIds.All(x => cache.ContainsKey(x));
                if (!canMakeNode)
                {
                    stack.Push(currentStatDefinitionId);

                    foreach (var dependentStatDefinitionId in dependentStatDefinitionIds)
                    {
                        stack.Push(dependentStatDefinitionId);
                    }

                    continue;
                }

                var termToCalculationNodeMapping = dependentStatDefinitionIds
                    .Select(x => KeyValuePair.Create(
                        _statDefinitionIdToTermMapping[x],
                        cache[x]))
                    .ToDictionary();

                cache[currentStatDefinitionId] = _statCalculationNodeFactory.Create(
                    expression,
                    termToCalculationNodeMapping);
            }

            return cache[statDefinitionId];
        }
    }
}