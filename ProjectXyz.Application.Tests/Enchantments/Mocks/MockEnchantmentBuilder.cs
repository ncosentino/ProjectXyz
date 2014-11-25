using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using Moq;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Application.Tests.Enchantments.Mocks
{
    public class MockEnchantmentBuilder
    {
        #region Fields
        private readonly Mock<IEnchantment> _enchantment;
        private readonly List<TimeSpan> _remaining;

        private Guid _statId;
        private Guid _calculationId;
        private double _value;
        private Guid _trigger;
        private Guid _statusTypeId;
        #endregion

        #region Constructors
        public MockEnchantmentBuilder()
        {
            _enchantment = new Mock<IEnchantment>();
            _remaining = new List<TimeSpan>(new[] { TimeSpan.Zero });
        }
        #endregion
        
        #region Methods
        public MockEnchantmentBuilder WithRemainingTime(params TimeSpan[] remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            return WithRemainingTime((IEnumerable<TimeSpan>)remainingTimes);
        }

        public MockEnchantmentBuilder WithRemainingTime(IEnumerable<TimeSpan> remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _remaining.Clear();
            _remaining.AddRange(remainingTimes);
            return this;
        }

        public MockEnchantmentBuilder WithStatId(Guid statId)
        {
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _statId = statId;
            return this;
        }

        public MockEnchantmentBuilder WithCalculationId(Guid calculationId)
        {
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _calculationId = calculationId;
            return this;
        }
        
        public MockEnchantmentBuilder WithValue(double value)
        {
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _value = value;
            return this;
        }

        public MockEnchantmentBuilder WithTrigger(Guid triggerId)
        {
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _trigger = triggerId;
            return this;
        }

        public MockEnchantmentBuilder WithStatusType(Guid statusTypeId)
        {
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _statusTypeId = statusTypeId;
            return this;
        }

        public IEnchantment Build()
        {
            Contract.Ensures(Contract.Result<IEnchantment>() != null);

            _enchantment
                .Setup(x => x.StatId)
                .Returns(_statId);
            _enchantment
                .Setup(x => x.CalculationId)
                .Returns(_calculationId);
            _enchantment
                .Setup(x => x.TriggerId)
                .Returns(_trigger);
            _enchantment
                .Setup(x => x.Value)
                .Returns(_value);

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
