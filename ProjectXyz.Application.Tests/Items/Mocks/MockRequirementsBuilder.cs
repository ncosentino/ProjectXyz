using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using Moq;

using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;

namespace ProjectXyz.Application.Tests.Items.Mocks
{
    public class MockRequirementsBuilder
    {
        #region Fields
        private readonly Mock<IRequirements> _mocked;
        private readonly List<IStat> _stats;
        #endregion

        #region Constructors
        public MockRequirementsBuilder()
        {
            _mocked = new Mock<IRequirements>();

            _stats = new List<IStat>();
        }
        #endregion

        #region Methods
        public MockRequirementsBuilder WithStats(params IStat[] stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<MockRequirementsBuilder>() != null);
            
            return WithStats((IEnumerable<IMutableStat>)stats);
        }

        public MockRequirementsBuilder WithStats(IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<MockRequirementsBuilder>() != null);

            _stats.AddRange(stats);
            return this;
        }

        public IRequirements Build()
        {
            Contract.Ensures(Contract.Result<IRequirements>() != null);

            _mocked
               .Setup(x => x.Class)
               .Returns(string.Empty);
            _mocked
                .Setup(x => x.Race)
                .Returns(string.Empty);
            _mocked
                .Setup(x => x.Level)
                .Returns(0);
            _mocked
                .Setup(x => x.Stats)
                .Returns(StatCollection.Create(_stats));

            return _mocked.Object;
        }
        #endregion
    }
}
