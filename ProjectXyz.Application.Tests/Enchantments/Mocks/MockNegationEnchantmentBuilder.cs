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
    public class MockNegationEnchantmentBuilder
    {
        #region Fields
        private readonly Mock<IOneShotNegateEnchantment> _enchantment;
        private readonly List<TimeSpan> _remaining;
        private readonly Guid _id;

        private Guid _statId;
        private Guid _trigger;
        private Guid _statusTypeId;
        #endregion

        #region Constructors
        public MockNegationEnchantmentBuilder()
        {
            _id = Guid.NewGuid();
            _statId = Guid.NewGuid();
            _trigger = Guid.NewGuid();
            _statusTypeId = Guid.NewGuid();

            _enchantment = new Mock<IOneShotNegateEnchantment>();
            _remaining = new List<TimeSpan>(new[] { TimeSpan.Zero });
        }
        #endregion
        
        #region Methods
        public MockNegationEnchantmentBuilder WithRemainingTime(params TimeSpan[] remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockNegationEnchantmentBuilder>() != null);

            return WithRemainingTime((IEnumerable<TimeSpan>)remainingTimes);
        }

        public MockNegationEnchantmentBuilder WithRemainingTime(IEnumerable<TimeSpan> remainingTimes)
        {
            Contract.Requires<ArgumentNullException>(remainingTimes != null);
            Contract.Ensures(Contract.Result<MockNegationEnchantmentBuilder>() != null);

            _remaining.Clear();
            _remaining.AddRange(remainingTimes);
            return this;
        }

        public MockNegationEnchantmentBuilder WithStatId(Guid statId)
        {
            Contract.Ensures(Contract.Result<MockNegationEnchantmentBuilder>() != null);

            _statId = statId;
            return this;
        }

        public MockNegationEnchantmentBuilder WithTrigger(Guid triggerId)
        {
            Contract.Ensures(Contract.Result<MockNegationEnchantmentBuilder>() != null);

            _trigger = triggerId;
            return this;
        }

        public MockNegationEnchantmentBuilder WithStatusType(Guid statusTypeId)
        {
            Contract.Ensures(Contract.Result<MockNegationEnchantmentBuilder>() != null);

            _statusTypeId = statusTypeId;
            return this;
        }

        public IOneShotNegateEnchantment Build()
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
