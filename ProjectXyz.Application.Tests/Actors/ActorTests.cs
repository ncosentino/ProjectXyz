using Moq;
using Xunit;

using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Tests.Actors.Mocks;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Application.Tests.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorTests
    {
        [Fact]
        public void Actor_CreateInstance_DefaultValues()
        {
            var builder = new Mock<IActorBuilder>();
            var data = new MockActorBuilder().Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var actor = Actor.Create(
                builder.Object,
                context.Object,
                data);

            Assert.Empty(actor.Stats);
            Assert.Empty(actor.Equipment);
        }
    }
}
