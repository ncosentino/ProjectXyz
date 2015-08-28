using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Moq;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Tests.Unit.Actors.Mocks
{
    public class MockActorBuilder
    {
        #region Fields
        private readonly Mock<IActorStore> _actor;
        private readonly List<IStat> _stats;
        #endregion

        #region Constructors
        public MockActorBuilder()
        {
            _actor = new Mock<IActorStore>();
            _stats = new List<IStat>();
        }
        #endregion

        #region Methods
        public MockActorBuilder WithStats(params IStat[] stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<MockActorBuilder>() != null);

            return WithStats((IEnumerable<IStat>)stats);
        }

        public MockActorBuilder WithStats(IEnumerable<IStat> stats)
        {
            Contract.Requires<ArgumentNullException>(stats != null);
            Contract.Ensures(Contract.Result<MockActorBuilder>() != null);

            _stats.AddRange(stats);
            return this;
        }

        public IActorStore Build()
        {
            Contract.Ensures(Contract.Result<IActorStore>() != null);

            _actor
                .Setup(x => x.Stats)
                .Returns(StatCollection.Create(_stats));

            return _actor.Object;
        }
        #endregion
    }
}
