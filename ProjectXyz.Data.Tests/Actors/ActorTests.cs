using Xunit;

using ProjectXyz.Data.Core.Actors;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Data.Tests.Actors
{
    [DataLayer]
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
