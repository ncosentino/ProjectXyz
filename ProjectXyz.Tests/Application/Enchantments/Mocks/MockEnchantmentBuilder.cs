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

namespace ProjectXyz.Tests.Application.Enchantments.Mocks
{
    public class MockEnchantmentBuilder
    {
        #region Fields
        private readonly Mock<IEnchantment> _enchantment;

        private TimeSpan _remaining;
        private string _statId;
        private string _calculationId;
        private double _value;
        private string _trigger;
        #endregion

        #region Constructors
        public MockEnchantmentBuilder()
        {
            _enchantment = new Mock<IEnchantment>();
            _statId = "Default";
            _calculationId = "Default";
            _trigger = "Default";
        }
        #endregion

        #region Methods
        public MockEnchantmentBuilder WithRemainingTime(TimeSpan remaining)
        {
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _remaining = remaining;
            return this;
        }

        public MockEnchantmentBuilder WithStatId(string statId)
        {
            Contract.Requires<ArgumentNullException>(statId != null);
            Contract.Requires<ArgumentException>(statId != string.Empty);
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _statId = statId;
            return this;
        }

        public MockEnchantmentBuilder WithCalculationId(string calculationId)
        {
            Contract.Requires<ArgumentNullException>(calculationId != null);
            Contract.Requires<ArgumentException>(calculationId != string.Empty);
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

        public MockEnchantmentBuilder WithTrigger(string trigger)
        {
            Contract.Requires<ArgumentNullException>(trigger != null);
            Contract.Requires<ArgumentException>(trigger != string.Empty);
            Contract.Ensures(Contract.Result<MockEnchantmentBuilder>() != null);

            _trigger = trigger;
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
                .Setup(x => x.Trigger)
                .Returns(_trigger);
            _enchantment
                .Setup(x => x.Value)
                .Returns(_value);
            _enchantment
                .Setup(x => x.RemainingDuration)
                .Returns(_remaining);

            return _enchantment.Object;
        }
        #endregion
    }
}
