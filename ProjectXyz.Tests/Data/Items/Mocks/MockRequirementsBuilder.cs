using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using Moq;

using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Items;

namespace ProjectXyz.Tests.Data.Items.Mocks
{
    public class MockRequirementsBuilder
    {
        #region Fields
        private readonly Mock<IRequirements> _requirements;
        private readonly List<IStat> _stats;
        #endregion

        #region Constructors
        public MockRequirementsBuilder()
        {
            _requirements = new Mock<IRequirements>();

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

            _requirements
                .Setup(x => x.Stats)
                .Returns(StatCollection<IStat>.Create(_stats));

            return _requirements.Object;
        }
        #endregion
    }
}
