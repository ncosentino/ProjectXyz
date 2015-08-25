using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Plugins.Enchantments.Expression;

namespace ProjectXyz.Application.Tests.Enchantments.Mocks
{
    public class MockExpressionEnchantmentBuilder
    {
        #region Fields
        private readonly Mock<IExpressionEnchantment> _enchantment;
        private readonly List<TimeSpan> _remaining;
        private readonly Guid _id;
        private readonly List<KeyValuePair<string, double>> _expressionValues;
        private readonly List<KeyValuePair<string, Guid>> _expressionStats;

        private Guid _statId;
        private double _value;
        private Guid _trigger;
        private Guid _statusTypeId;
        private string _expression;
        #endregion

        #region Constructors
        public MockExpressionEnchantmentBuilder()
        {
            _id = Guid.NewGuid();
            _statId = Guid.NewGuid();
            _trigger = Guid.NewGuid();
            _statusTypeId = Guid.NewGuid();
            
            _enchantment = new Mock<IExpressionEnchantment>();
            _remaining = new List<TimeSpan>(new[] { TimeSpan.Zero });
            _expressionValues = new List<KeyValuePair<string, double>>();
            _expressionStats = new List<KeyValuePair<string, Guid>>();
        }
        #endregion
        
        #region Methods
        public MockExpressionEnchantmentBuilder WithRemainingTime(params TimeSpan[] remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            return WithRemainingTime((IEnumerable<TimeSpan>)remainingTimes);
        }

        public MockExpressionEnchantmentBuilder WithRemainingTime(IEnumerable<TimeSpan> remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _remaining.Clear();
            _remaining.AddRange(remainingTimes);
            return this;
        }

        public MockExpressionEnchantmentBuilder WithStatId(Guid statId)
        {
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _statId = statId;
            return this;
        }

        public MockExpressionEnchantmentBuilder WithStat(string idForExpression, Guid statId)
        {
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _expressionStats.Add(new KeyValuePair<string, Guid>(idForExpression, statId));
            return this;
        }

        public MockExpressionEnchantmentBuilder WithValue(string idForExpression, double value)
        {
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _expressionValues.Add(new KeyValuePair<string, double>(idForExpression, value));
            return this;
        }

        public MockExpressionEnchantmentBuilder WithExpression(string expression)
        {
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _expression = expression;
            return this;
        }

        public MockExpressionEnchantmentBuilder WithTrigger(Guid triggerId)
        {
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _trigger = triggerId;
            return this;
        }

        public MockExpressionEnchantmentBuilder WithStatusType(Guid statusTypeId)
        {
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _statusTypeId = statusTypeId;
            return this;
        }

        public MockExpressionEnchantmentBuilder WhenUpdatedElapsedTime(TimeSpan elapsedTime, Action<Mock<IExpressionEnchantment>> callback)
        {
            Contract.Ensures(Contract.Result<MockExpressionEnchantmentBuilder>() != null);

            _enchantment
                .Setup(x => x.UpdateElapsedTime(elapsedTime))
                .Callback(() =>
                {
                    callback.Invoke(_enchantment);
                });

            return this;
        }

        public IExpressionEnchantment Build()
        {
            Contract.Ensures(Contract.Result<IEnchantment>() != null);
            
            _enchantment
                .Setup(x => x.Id)
                .Returns(_id);
            _enchantment
                .Setup(x => x.StatId)
                .Returns(_statId);
            _enchantment
                .Setup(x => x.TriggerId)
                .Returns(_trigger);
            _enchantment
                .Setup(x => x.Expression)
                .Returns(_expression);
            _enchantment
                .Setup(x => x.StatExpressionIds)
                .Returns(_expressionStats.Select(x => x.Key));
            _enchantment
                .Setup(x => x.ValueExpressionIds)
                .Returns(_expressionValues.Select(x => x.Key));

            foreach (var kvp in _expressionValues)
            {
                _enchantment
                    .Setup(x => x.GetValueForValueExpressionId(kvp.Key))
                    .Returns(kvp.Value);
            }

            foreach (var kvp in _expressionStats)
            {
                _enchantment
                    .Setup(x => x.GetStatIdForStatExpressionId(kvp.Key))
                    .Returns(kvp.Value);
            }

            if (_remaining.Count == 1)
            {
                _enchantment
                    .Setup(x => x.RemainingDuration)
                    .Returns(_remaining[0]);
            }
            else
            {
                var sequence = _enchantment.SetupSequence(x => x.RemainingDuration);
                foreach (var remainingTime in _remaining)
                {
                    sequence = sequence.Returns(remainingTime);
                }
            }

            _enchantment
                .Setup(x => x.StatusTypeId)
                .Returns(_statusTypeId);

            return _enchantment.Object;
        }
        #endregion
    }
}
