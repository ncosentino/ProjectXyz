using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Enchantments.Calculations;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemTests
    {
        #region Methods
        [Fact]
        public void Create_ValidArguments_DefaultValues()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = new Mock<IItemContext>(MockBehavior.Strict);

            // Execute
            var result = Item.Create(
                itemContext.Object,
                id,
                itemDefinitionId,
                itemMetaData.Object,
                itemRequirements.Object,
                Enumerable.Empty<IStat>(),
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(itemDefinitionId, result.ItemDefinitionId);
            Assert.Empty(result.Enchantments);
            Assert.Empty(result.SocketedItems);
            Assert.Empty(result.EquippableSlotIds);
            Assert.Equal(itemRequirements.Object, result.Requirements);
        }

        [Fact]
        public void GetStats_NewlyCreatedNoStatsNoEnchantments_RunsCalculation()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var enchantmentCalculatorResult = new Mock<IEnchantmentCalculatorResult>(MockBehavior.Strict);
            enchantmentCalculatorResult
                .Setup(x => x.Enchantments)
                .Returns(Enumerable.Empty<IEnchantment>());
            enchantmentCalculatorResult
                .Setup(x => x.Stats)
                .Returns(StatCollection.Create());

            var enchantmentCalculator = new Mock<IEnchantmentCalculator>(MockBehavior.Strict);
            enchantmentCalculator
                .Setup(x => x.Calculate(It.IsAny<IStatCollection>(), It.IsAny<IEnumerable<IEnchantment>>()))
                .Returns(enchantmentCalculatorResult.Object);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<IStatSocketType>());

            var itemContext = new Mock<IItemContext>(MockBehavior.Strict);
            itemContext
                .Setup(x => x.EnchantmentCalculator)
                .Returns(enchantmentCalculator.Object);
            itemContext
                .Setup(x => x.StatSocketTypeRepository)
                .Returns(statSocketTypeRepository.Object);

            var item = Item.Create(
                itemContext.Object,
                id,
                itemDefinitionId,
                itemMetaData.Object,
                itemRequirements.Object,
                Enumerable.Empty<IStat>(),
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            // Execute
            var result = item.Stats;

            // Assert
            Assert.NotEmpty(result);

            enchantmentCalculatorResult.Verify(x => x.Enchantments, Times.Once);
            enchantmentCalculatorResult.Verify(x => x.Stats, Times.Once);

            enchantmentCalculator.Verify(x => x.Calculate(It.IsAny<IStatCollection>(), It.IsAny<IEnumerable<IEnchantment>>()), Times.Once);

            statSocketTypeRepository.Verify(x => x.GetAll(), Times.Once);

            itemContext.Verify(x => x.EnchantmentCalculator, Times.Once);
            itemContext.Verify(x => x.StatSocketTypeRepository, Times.Once);
        }
        #endregion
    }
}
