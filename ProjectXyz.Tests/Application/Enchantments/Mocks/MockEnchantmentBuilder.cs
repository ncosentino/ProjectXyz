using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        #region Constructors
        public MockEnchantmentBuilder()
        {
            _enchantment = new Mock<IEnchantment>();
        }
        #endregion

        #region Methods
        public MockEnchantmentBuilder WithRemainingTime(TimeSpan remaining)
        {
            _remaining = remaining;
            return this;
        }

        public MockEnchantmentBuilder WithStatId(string statId)
        {
            _statId = statId;
            return this;
        }

        public MockEnchantmentBuilder WithCalculationId(string calculationId)
        {
            _calculationId = calculationId;
            return this;
        }

        public MockEnchantmentBuilder WithValue(double value)
        {
            _value = value;
            return this;
        }

        public IEnchantment Build()
        {
            _enchantment
                .Setup(x => x.StatId)
                .Returns(_statId);
            _enchantment
                .Setup(x => x.CalculationId)
                .Returns(_calculationId);
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
