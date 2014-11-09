﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Data.Core.Stats;

namespace ProjectXyz.Tests.Data.Actors.Mocks
{
    public class MockActorBuilder
    {
        #region Fields
        private readonly Mock<IActor> _actor;
        private readonly List<IStat> _stats;
        #endregion

        #region Constructors
        public MockActorBuilder()
        {
            _actor = new Mock<IActor>();
            _stats = new List<IStat>();
        }
        #endregion

        #region Methods
        public MockActorBuilder WithStats(params IStat[] stats)
        {
            return WithStats((IEnumerable<IStat>)stats);
        }

        public MockActorBuilder WithStats(IEnumerable<IStat> stats)
        {
            _stats.AddRange(stats);
            return this;
        }

        public IActor Build()
        {
            _actor
                .Setup(x => x.Stats)
                .Returns(StatCollection.Create(_stats));

            return _actor.Object;
        }
        #endregion
    }
}
