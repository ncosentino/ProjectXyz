using Xunit;

using ProjectXyz.Data.Core.Actors;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Tests.Data.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorTests
    {
        [Fact]
        public void Defaults()
        {
            var actor = Actor.Create();

            Assert.Empty(actor.Stats);
        }
    }
}
