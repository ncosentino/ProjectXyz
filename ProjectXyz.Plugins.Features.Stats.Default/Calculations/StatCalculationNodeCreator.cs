using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Stats.Calculations;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.Stats.Default.Calculations
{
    public sealed class StatCalculationNodeCreator : IStatCalculationNodeCreator
    {
        private readonly IStatCalculationNodeFactory _statCalculationNodeFactory;
        private readonly IExpressionStatDefinitionDependencyFinder _expressionStatDefinitionDependencyFinder;
        private readonly IStatDefinitionToTermConverter _statDefinitionToTermConverter;
        private readonly IStatDefinitionToCalculationConverter _statDefinitionToCalculationConverter;

        public StatCalculationNodeCreator(
            IStatCalculationNodeFactory statCalculationNodeFactory,
            IExpressionStatDefinitionDependencyFinder expressionStatDefinitionDependencyFinder,
            IStatDefinitionToTermConverter statDefinitionToTermConverter,
            IStatDefinitionToCalculationConverter statDefinitionToCalculationConverter)
        {
            _statCalculationNodeFactory = statCalculationNodeFactory;
            _expressionStatDefinitionDependencyFinder = expressionStatDefinitionDependencyFinder;
            _statDefinitionToTermConverter = statDefinitionToTermConverter;
            _statDefinitionToCalculationConverter = statDefinitionToCalculationConverter;
        }

        public IStatCalculationNode Create(
            IReadOnlyCollection<IStatExpressionInterceptor> statExpressionInterceptors,
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
                    : _statDefinitionToCalculationConverter.ContainsKey(currentStatDefinitionId)
                        ? _statDefinitionToCalculationConverter[currentStatDefinitionId]
                        : "0";

                expression = statExpressionInterceptors
                    .Aggregate(
                        expression,
                        (c, interceptor) => interceptor.Intercept(
                            currentStatDefinitionId,
                            c));

                var dependentStatDefinitionIds = _expressionStatDefinitionDependencyFinder.FindDependencies(
                    _statDefinitionToTermConverter,
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
                        _statDefinitionToTermConverter[x],
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