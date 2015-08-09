using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using Moq;

using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;

namespace ProjectXyz.Application.Tests.Enchantments.Mocks
{
    public class MockAdditiveEnchantmentBuilder
    {
        #region Fields
        private readonly Mock<IAdditiveEnchantment> _enchantment;
        private readonly List<TimeSpan> _remaining;
        private readonly Guid _id;

        private Guid _statId;
        private double _value;
        private Guid _trigger;
        private Guid _statusTypeId;
        #endregion

        #region Constructors
        public MockAdditiveEnchantmentBuilder()
        {
            _id = Guid.NewGuid();
            _statId = Guid.NewGuid();
            _trigger = Guid.NewGuid();
            _statusTypeId = Guid.NewGuid();
            
            _enchantment = new Mock<IAdditiveEnchantment>();
            _remaining = new List<TimeSpan>(new[] { TimeSpan.Zero });
        }
        #endregion
        
        #region Methods
        public MockAdditiveEnchantmentBuilder WithRemainingTime(params TimeSpan[] remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockAdditiveEnchantmentBuilder>() != null);

            return WithRemainingTime((IEnumerable<TimeSpan>)remainingTimes);
        }

        public MockAdditiveEnchantmentBuilder WithRemainingTime(IEnumerable<TimeSpan> remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockAdditiveEnchantmentBuilder>() != null);

            _remaining.Clear();
            _remaining.AddRange(remainingTimes);
            return this;
        }

        public MockAdditiveEnchantmentBuilder WithStatId(Guid statId)
        {
            Contract.Ensures(Contract.Result<MockAdditiveEnchantmentBuilder>() != null);

            _statId = statId;
            return this;
        }

        public MockAdditiveEnchantmentBuilder WithValue(double value)
        {
            Contract.Ensures(Contract.Result<MockAdditiveEnchantmentBuilder>() != null);

            _value = value;
            return this;
        }

        public MockAdditiveEnchantmentBuilder WithTrigger(Guid triggerId)
        {
            Contract.Ensures(Contract.Result<MockAdditiveEnchantmentBuilder>() != null);

            _trigger = triggerId;
            return this;
        }

        public MockAdditiveEnchantmentBuilder WithStatusType(Guid statusTypeId)
        {
            Contract.Ensures(Contract.Result<MockAdditiveEnchantmentBuilder>() != null);

            _statusTypeId = statusTypeId;
            return this;
        }

        public IAdditiveEnchantment Build()
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
