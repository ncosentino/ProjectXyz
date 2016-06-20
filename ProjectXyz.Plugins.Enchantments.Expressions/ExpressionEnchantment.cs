using System.Collections.Generic;
using ProjectXyz.Framework.Interface;
using ProjectXyz.Framework.Interface.Collections;

namespace ProjectXyz.Plugins.Enchantments.Expressions
{
    public sealed class ExpressionEnchantment : IExpressionEnchantment
    {
        #region Fields
        private readonly Dictionary<string, IIdentifier> _expressionStatDefinitionIds;
        private readonly Dictionary<string, double> _expressionValues;
        #endregion

        #region Constructors
        public ExpressionEnchantment(
            IIdentifier statusTypeId,
            IIdentifier triggerId,
            IIdentifier statDefinitionId,
            IIdentifier weatherGroupingId,
            string expression,
            int calculationPriority,
            IEnumerable<KeyValuePair<string, IIdentifier>> expressionStatDefinitionIds,
            IEnumerable<KeyValuePair<string, double>> expressionValues)
        {
            StatusTypeId = statusTypeId;
            TriggerId = triggerId;
            StatDefinitionId = statDefinitionId;
            WeatherGroupingId = weatherGroupingId;
            Expression = expression;
            CalculationPriority = calculationPriority;
            _expressionStatDefinitionIds = expressionStatDefinitionIds.ToDictionary();
            _expressionValues = expressionValues.ToDictionary();
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public IIdentifier StatusTypeId { get; }

        /// <inheritdoc />
        public IIdentifier TriggerId { get; }

        /// <inheritdoc />
        public IIdentifier StatDefinitionId { get; }

        /// <inheritdoc />
        public IIdentifier WeatherGroupingId { get; }

        /// <inheritdoc />
        public string Expression { get; }

        /// <inheritdoc />
        public int CalculationPriority { get; }

        /// <inheritdoc />
        public IEnumerable<string> StatExpressionIds => _expressionStatDefinitionIds.Keys;

        /// <inheritdoc />
        public IEnumerable<string> ValueExpressionIds => _expressionValues.Keys;
        #endregion

        #region Methods
        public IIdentifier GetStatDefinitionIdForStatExpressionId(string statExpressionId)
        {
            return _expressionStatDefinitionIds[statExpressionId];
        }

        public double GetValueForValueExpressionId(string valueExpressionId)
        {
            return _expressionValues[valueExpressionId];
        }
        #endregion
    }
}
