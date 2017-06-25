﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Application.Stats.Interface.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;
using ProjectXyz.Framework.Shared;

namespace ProjectXyz.Application.Stats.Core.Calculations
{
    public sealed class StatCalculationNodeCreator : IStatCalculationNodeCreator
    {
        private readonly IStatCalculationNodeFactory _statCalculationNodeFactory;
        private readonly IExpressionStatDefinitionDependencyFinder _expressionStatDefinitionDependencyFinder;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToTermMapping;
        private readonly IReadOnlyDictionary<IIdentifier, string> _statDefinitionIdToCalculationMapping;

        public StatCalculationNodeCreator(
            IStatCalculationNodeFactory statCalculationNodeFactory,
            IExpressionStatDefinitionDependencyFinder expressionStatDefinitionDependencyFinder,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToTermMapping,
            IReadOnlyDictionary<IIdentifier, string> statDefinitionIdToCalculationMapping)
        {
            _statCalculationNodeFactory = statCalculationNodeFactory;
            _expressionStatDefinitionDependencyFinder = expressionStatDefinitionDependencyFinder;
            _statDefinitionIdToTermMapping = statDefinitionIdToTermMapping;
            _statDefinitionIdToCalculationMapping = statDefinitionIdToCalculationMapping;
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
                    : _statDefinitionIdToCalculationMapping.ContainsKey(currentStatDefinitionId)
                        ? _statDefinitionIdToCalculationMapping[currentStatDefinitionId]
                        : "0";

                expression = statExpressionInterceptors
                    .Aggregate(
                        expression, 
                        (c, interceptor) => interceptor.Intercept(
                            currentStatDefinitionId, 
                            c));

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