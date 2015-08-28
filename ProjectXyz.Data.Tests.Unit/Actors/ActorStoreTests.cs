using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Actors;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Tests.Unit.Actors
{
    [DataLayer]
    [Actors]
    public class ActorStoreTests
    {
        [Fact]
        public void Actor_CreateInstance_DefaultValues()
        {
            var actor = ActorStore.Create();

            Assert.Empty(actor.Stats);
        }
    }
}
