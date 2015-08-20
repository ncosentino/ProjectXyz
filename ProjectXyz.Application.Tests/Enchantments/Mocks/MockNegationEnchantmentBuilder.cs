using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Plugins.Enchantments.OneShotNegate;

namespace ProjectXyz.Application.Tests.Enchantments.Mocks
{
    public class MockNegationEnchantmentBuilder
    {
        #region Fields
        private readonly Mock<IOneShotNegateEnchantment> _enchantment;
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
        }
        #endregion
        
        #region Methods
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
            _enchantment
                .Setup(x => x.StatusTypeId)
                .Returns(_statusTypeId);

            return _enchantment.Object;
        }
        #endregion
    }
}
