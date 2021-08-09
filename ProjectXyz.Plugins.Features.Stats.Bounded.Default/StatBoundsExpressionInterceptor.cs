using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Stats.Calculations;

namespace ProjectXyz.Plugins.Features.Stats.Bounded.Default
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
            if (!_statDefinitionIdToBoundsMapping.TryGetValue(statDefinitionId, out var statBounds))
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