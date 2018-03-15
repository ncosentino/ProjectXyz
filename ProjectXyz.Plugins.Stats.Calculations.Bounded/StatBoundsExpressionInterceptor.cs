using System.Collections.Generic;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Stats.Calculations;
using ProjectXyz.Framework.Extensions.Collections;
using ProjectXyz.Plugins.Features.BoundedStats.Api;

namespace ProjectXyz.Plugins.Features.BoundedStats
{
    public sealed class StatBoundsExpressionInterceptor : IStatExpressionInterceptor
    {
        #region Fields
        private readonly IReadOnlyDictionary<IIdentifier, IStatBounds> _statDefinitionIdToBoundsMapping;
        #endregion

        #region Constructors
        public StatBoundsExpressionInterceptor(
            IEnumerable<KeyValuePair<IIdentifier, IStatBounds>> statDefinitionIdToBoundsMapping,
            int priority)
            : this(statDefinitionIdToBoundsMapping.ToDictionary(), priority)
        {
        }

        public StatBoundsExpressionInterceptor(
            IReadOnlyDictionary<IIdentifier, IStatBounds> statDefinitionIdToBoundsMapping,
            int priority)
        {
            _statDefinitionIdToBoundsMapping = statDefinitionIdToBoundsMapping;
            Priority = priority;
        }
        #endregion

        #region Methods
        public int Priority { get; }

        public string Intercept(
            IIdentifier statDefinitionId, 
            string expression)
        {
            IStatBounds statBounds;
            if (!_statDefinitionIdToBoundsMapping.TryGetValue(statDefinitionId, out statBounds))
            {
                return expression;
            }

            if (!string.IsNullOrWhiteSpace(statBounds.MinimumExpression))
            {
                expression = $"MAX({expression}, {statBounds.MinimumExpression})";
            }

            if (!string.IsNullOrWhiteSpace(statBounds.MaximumExpression))
            {
                expression = $"MIN({expression}, {statBounds.MaximumExpression})";
            }

            return expression;
        }
        #endregion
    }
}