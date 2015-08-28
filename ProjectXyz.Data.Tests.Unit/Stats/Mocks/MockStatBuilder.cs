using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Tests.Unit.Stats.Mocks
{
    public class MockStatBuilder
    {
        #region Fields
        private readonly Mock<IStat> _mock;

        private Guid _statId;
        private double _value;
        #endregion

        #region Constructors
        public MockStatBuilder()
        {
            _mock = new Mock<IStat>();

            _value = 0;
        }
        #endregion

        #region Methods
        public MockStatBuilder WithStatId(Guid statId)
        {
            Contract.Ensures(Contract.Result<MockStatBuilder>() != null);

            _statId = statId;
            return this;
        }

        public MockStatBuilder WithValue(double value)
        {
            Contract.Ensures(Contract.Result<MockStatBuilder>() != null);

            _value = value;
            return this;
        }

        public IStat Build()
        {
            Contract.Ensures(Contract.Result<IStat>() != null);

            _mock
                .Setup(x => x.StatDefinitionId)
                .Returns(_statId);
            _mock
                .Setup(x => x.Value)
                .Returns(_value);
            
            return _mock.Object;
        }
        #endregion
    }
}
