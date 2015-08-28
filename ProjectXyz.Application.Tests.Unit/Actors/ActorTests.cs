using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Actors;
using ProjectXyz.Application.Core.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Actors;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Data.Interface.Actors;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Actors
{
    [ApplicationLayer]
    [Actors]
    public class ActorTests
    {
        [Fact]
        public void Create_ValidParameters_DefaultValues()
        {
            // Setup
            var data = new Mock<IActorStore>(MockBehavior.Strict);

            var enchantmentCalculatorResultFactory = new Mock<IEnchantmentCalculatorResultFactory>(MockBehavior.Strict);

            var enchantmentContext = new Mock<IEnchantmentContext>(MockBehavior.Strict);

            var context = new Mock<IActorContext>();
            context.
                Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create(
                    enchantmentContext.Object,
                    enchantmentCalculatorResultFactory.Object,
                    Enumerable.Empty<IEnchantmentTypeCalculator>()));

            // Excecute
            var actor = Actor.Create(
                context.Object,
                data.Object);

            // Assert
            Assert.Empty(actor.Stats);
            Assert.Empty(actor.Equipment);
        }
    }
}
