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
            // Setup
            var id = Guid.NewGuid();

            // Execute
            var actor = ActorStore.Create(id);

            // Assert
            Assert.Empty(actor.Stats);
            Assert.Equal(id, actor.Id);
        }
    }
}
