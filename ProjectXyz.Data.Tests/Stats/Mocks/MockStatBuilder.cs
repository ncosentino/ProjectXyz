using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using Moq;

using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Tests.Stats.Mocks
{
    public class MockStatBuilder
    {
        #region Fields
        private readonly Mock<IStat> _mock;

        private string _statId;
        private double _value;
        #endregion

        #region Constructors
        public MockStatBuilder()
        {
            _mock = new Mock<IStat>();

            _statId = "Default";
            _value = 0;
        }
        #endregion

        #region Methods
        public MockStatBuilder WithStatId(string statId)
        {
            Contract.Requires<ArgumentNullException>(statId != null);
            Contract.Requires<ArgumentException>(statId != string.Empty);
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
                .Setup(x => x.Id)
                .Returns(_statId);
            _mock
                .Setup(x => x.Value)
                .Returns(_value);
            
            return _mock.Object;
        }
        #endregion
    }
}
