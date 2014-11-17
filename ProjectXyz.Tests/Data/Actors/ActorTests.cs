﻿using Xunit;

using ProjectXyz.Data.Core.Actors;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Tests.Data.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorTests
    {
        [Fact]
        public void Actor_CreateInstance_DefaultValues()
        {
            var actor = Actor.Create();

            Assert.Empty(actor.Stats);
        }
    }
}
