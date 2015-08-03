using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Tests.Actors.Mocks;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorTests
    {
        [Fact]
        public void Actor_CreateInstance_DefaultValues()
        {
            var data = new MockActorBuilder().Build();
            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create(
                    StatFactory.Create(),
                    new Mock<IStatusNegationRepository>().Object));

            var actor = Actor.Create(
                context.Object,
                data);

            Assert.Empty(actor.Stats);
            Assert.Empty(actor.Equipment);
        }
    }
}
