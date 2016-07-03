using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Application.Interface.Stats.Calculations;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Application.Core.Stats.Calculations
{
    public sealed class StatBoundsExpressionInterceptor : IStatExpressionInterceptor
    {
        #region Fields
        private readonly IReadOnlyDictionary<IIdentifier, IStatBounds> _statDefinitionIdToBoundsMapping;
        #endregion

        #region Constructors
        public StatBoundsExpressionInterceptor(IEnumerable<KeyValuePair<IIdentifier, IStatBounds>> statDefinitionIdToBoundsMapping)
            : this(statDefinitionIdToBoundsMapping.ToDictionary())
        {
        }

        public StatBoundsExpressionInterceptor(IReadOnlyDictionary<IIdentifier, IStatBounds> statDefinitionIdToBoundsMapping)
        {
            _statDefinitionIdToBoundsMapping = statDefinitionIdToBoundsMapping;
        }
        #endregion

        #region Properties
        public static IStatExpressionInterceptor None { get; } = new StatBoundsExpressionInterceptor(Enumerable.Empty<KeyValuePair<IIdentifier, IStatBounds>>());
        #endregion

        #region Methods
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