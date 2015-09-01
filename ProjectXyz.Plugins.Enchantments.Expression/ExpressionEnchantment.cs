using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Plugins.Enchantments.Expression
{
    public sealed class ExpressionEnchantment :
        Enchantment,
        IExpressionEnchantment
    {
        #region Constants
        // FIXME: this should be a constant value defined somewhere
        private static readonly Guid ENCHANTMENT_TYPE_ID = Guid.NewGuid();
        #endregion

        #region Fields
        private readonly Guid _statId;
        private readonly string _expression;
        private readonly int _calculationPriority;
        private readonly Dictionary<string, Guid> _expressionStatIds;
        private readonly Dictionary<string, double> _expressionValues;

        private TimeSpan _remainingDuration;
        #endregion

        #region Constructors
        private ExpressionEnchantment(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid weatherTypeGroupingId,
            TimeSpan remainingDuration,
            Guid statId,
            string expression,
            int calculationPriority,
            IEnumerable<KeyValuePair<string, Guid>> expressionStatIds,
            IEnumerable<KeyValuePair<string, double>> expressionValues)
            : base(
                id,
                statusTypeId,
                triggerId,
                ENCHANTMENT_TYPE_ID,
                weatherTypeGroupingId)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(expression));
            Contract.Requires<ArgumentNullException>(expressionStatIds != null);
            Contract.Requires<ArgumentNullException>(expressionValues != null);

            _statId = statId;
            _remainingDuration = remainingDuration;
            _expression = expression;
            _calculationPriority = calculationPriority;
            
            _expressionStatIds = new Dictionary<string, Guid>();
            foreach (var kvp in expressionStatIds)
            {
                _expressionStatIds.Add(kvp.Key, kvp.Value);
            }

            _expressionValues = new Dictionary<string, double>();
            foreach (var kvp in expressionValues)
            {
                _expressionValues.Add(kvp.Key, kvp.Value);
            }
        }
        #endregion

        #region Properties
        /// <inheritdoc />
        public Guid StatId 
        {
            get { return _statId; } 
        }

        /// <inheritdoc />
        public string Expression
        {
            get { return _expression; }
        }

        /// <inheritdoc />
        public int CalculationPriority
        {
            get { return _calculationPriority; }
        }

        /// <inheritdoc />
        public IEnumerable<string> StatExpressionIds
        {
            get { return _expressionStatIds.Keys; }
        }

        /// <inheritdoc />
        public IEnumerable<string> ValueExpressionIds
        {
            get { return _expressionValues.Keys; }
        }

        /// <inheritdoc />
        public TimeSpan RemainingDuration 
        { 
            get { return _remainingDuration; } 
        }
        #endregion

        #region Methods
        public static IExpressionEnchantment Create(
            Guid id,
            Guid statusTypeId,
            Guid triggerId,
            Guid weatherTypeGroupingId,
            TimeSpan remainingDuration,
            Guid statId,
            string expression,
            int calculationPriority,
            IEnumerable<KeyValuePair<string, Guid>> expressionStatIds,
            IEnumerable<KeyValuePair<string, double>> expressionValues)
        {
            Contract.Requires<ArgumentException>(id != Guid.Empty);
            Contract.Requires<ArgumentException>(triggerId != Guid.Empty);
            Contract.Requires<ArgumentException>(statusTypeId != Guid.Empty);
            Contract.Requires<ArgumentException>(weatherTypeGroupingId != Guid.Empty);
            Contract.Requires<ArgumentOutOfRangeException>(remainingDuration >= TimeSpan.Zero);
            Contract.Requires<ArgumentException>(statId != Guid.Empty);
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(expression));
            Contract.Requires<ArgumentNullException>(expressionStatIds != null);
            Contract.Requires<ArgumentNullException>(expressionValues != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantment>() != null);

            var enchantment = new ExpressionEnchantment(
                id,
                statusTypeId,
                triggerId,
                weatherTypeGroupingId,
                remainingDuration,
                statId,
                expression,
                calculationPriority,
                expressionStatIds,
                expressionValues);
            return enchantment;
        }

        public Guid GetStatIdForStatExpressionId(string statExpressionId)
        {
            return _expressionStatIds[statExpressionId];
        }

        public double GetValueForValueExpressionId(string valueExpressionId)
        {
            return _expressionValues[valueExpressionId];
        }

        /// <inheritdoc />
        public override void UpdateElapsedTime(TimeSpan elapsedTime)
        {
            if (_remainingDuration == TimeSpan.Zero)
            {
                return;
            }

            _remainingDuration = TimeSpan.FromMilliseconds(Math.Max(
                (_remainingDuration - elapsedTime).TotalMilliseconds,
                0));

            if (_remainingDuration == TimeSpan.Zero)
            {
                OnExpired();
            }
        }
        #endregion
    }
}
