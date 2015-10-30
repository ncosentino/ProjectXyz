using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items.Sockets;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Application.Interface.Stats;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Items.Sockets
{
    [ApplicationLayer]
    [Items]
    public class SocketPatternItemConverterTests
    {
        #region Methods
        [Fact]
        public void ConvertItem_NoOverridingBaseItem_OriginalItemInformation()
        {
            // Setup
            var itemDefinitionId = Guid.NewGuid();
            var itemInventoryGraphicResourceId = Guid.NewGuid();
            var itemMagicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var itemMaterialTypeId = Guid.NewGuid();
            var itemSocketTypeId = Guid.NewGuid();
            var socketPatternNameStringResourceId = Guid.NewGuid();
            var socketPatternId = Guid.NewGuid();

            var baseItemStat = new Mock<IStat>(MockBehavior.Strict);
            var baseItemStats = new[]
            {
                baseItemStat.Object
            };

            var baseItemEnchantment = new Mock<IEnchantment>(MockBehavior.Strict);

            var baseItemEnchantments = new Mock<IEnchantmentCollection>(MockBehavior.Strict);
            baseItemEnchantments
                .Setup(x => x.GetEnumerator())
                .Returns(new List<IEnchantment>() { baseItemEnchantment.Object }.GetEnumerator());


            var randomizer = new Mock<IRandom>(MockBehavior.Strict);

            var statMerger = new Mock<IStatMerger>(MockBehavior.Strict);
            statMerger
                .Setup(x => x.MergeStats(baseItemStats, Enumerable.Empty<IStat>()))
                .Returns(baseItemStats);

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemMetaDataFactory = new Mock<IItemMetaDataFactory>(MockBehavior.Strict);
            itemMetaDataFactory
                .Setup(x => x.Create(
                    itemInventoryGraphicResourceId,
                    itemMagicTypeId,
                    itemTypeId,
                    itemMaterialTypeId,
                    itemSocketTypeId))
                .Returns(itemMetaData.Object);

            var itemNamePart = new Mock<IItemNamePart>(MockBehavior.Strict);

            var itemNamePartFactory = new Mock<IItemNamePartFactory>(MockBehavior.Strict);
            itemNamePartFactory
                .Setup(x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    socketPatternNameStringResourceId,
                    0))
                .Returns(itemNamePart.Object);

            var statGenerator = new Mock<IStatGenerator>(MockBehavior.Strict);
            statGenerator
                .Setup(x => x.GenerateStats(randomizer.Object, It.Is<IEnumerable<IStatRange>>(stats => !stats.Any())))
                .Returns(Enumerable.Empty<IStat>());

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>(MockBehavior.Strict);

            var socketPatternDefinitionStatRepository = new Mock<ISocketPatternDefinitionStatRepository>(MockBehavior.Strict);
            socketPatternDefinitionStatRepository
                .Setup(x => x.GetBySocketPatternId(socketPatternId))
                .Returns(Enumerable.Empty<ISocketPatternStat>());

            var socketPatternDefinitionEnchantmentRepository = new Mock<ISocketPatternDefinitionEnchantmentRepository>(MockBehavior.Strict);
            socketPatternDefinitionEnchantmentRepository
                .Setup(x => x.GetBySocketPatternId(socketPatternId))
                .Returns(Enumerable.Empty<ISocketPatternEnchantment>());

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = new Mock<IItemContext>(MockBehavior.Strict);

            var socketedItems = new Mock<IItemCollection>(MockBehavior.Strict);

            var itemToConvert = new Mock<IItem>(MockBehavior.Strict);
            itemToConvert
                .Setup(x => x.InventoryGraphicResourceId)
                .Returns(itemInventoryGraphicResourceId);
            itemToConvert
                .Setup(x => x.MagicTypeId)
                .Returns(itemMagicTypeId);
            itemToConvert
                .Setup(x => x.ItemTypeId)
                .Returns(itemTypeId);
            itemToConvert
                .Setup(x => x.MaterialTypeId)
                .Returns(itemMaterialTypeId);
            itemToConvert
                .Setup(x => x.SocketTypeId)
                .Returns(itemSocketTypeId);
            itemToConvert
                .Setup(x => x.BaseStats)
                .Returns(baseItemStats);
            itemToConvert
                .Setup(x => x.Enchantments)
                .Returns(baseItemEnchantments.Object);
            itemToConvert
                .Setup(x => x.ItemDefinitionId)
                .Returns(itemDefinitionId);
            itemToConvert
                .Setup(x => x.Requirements)
                .Returns(itemRequirements.Object);
            itemToConvert
                .Setup(x => x.Affixes)
                .Returns(Enumerable.Empty<IItemAffix>());
            itemToConvert
                .Setup(x => x.SocketedItems)
                .Returns(socketedItems.Object);
            itemToConvert
                .Setup(x => x.EquippableSlotIds)
                .Returns(Enumerable.Empty<Guid>());

            var socketPatternDefinition = new Mock<ISocketPatternDefinition>(MockBehavior.Strict);
            socketPatternDefinition
                .Setup(x => x.InventoryGraphicResourceId)
                .Returns((Guid?)null);
            socketPatternDefinition
                .Setup(x => x.MagicTypeId)
                .Returns((Guid?)null);
            socketPatternDefinition
                .Setup(x => x.NameStringResourceId)
                .Returns(socketPatternNameStringResourceId);
            socketPatternDefinition
                .Setup(x => x.Id)
                .Returns(socketPatternId);

            var expectedResult = new Mock<IItem>(MockBehavior.Strict);

            var itemFactory = new Mock<IItemFactory>(MockBehavior.Strict);
            itemFactory
                .Setup(x => x.Create(
                    itemContext.Object,
                    It.IsAny<Guid>(),
                    itemDefinitionId,
                    itemMetaData.Object,
                    new[] { itemNamePart.Object },
                    itemRequirements.Object,
                    baseItemStats,
                    new[] { baseItemEnchantment.Object },
                    Enumerable.Empty<IItemAffix>(),
                    socketedItems.Object,
                    Enumerable.Empty<Guid>()))
                .Returns(expectedResult.Object);

            var converter = SocketPatternItemConverter.Create(
                statMerger.Object,
                itemFactory.Object,
                itemMetaDataFactory.Object,
                itemNamePartFactory.Object,
                statGenerator.Object,
                enchantmentGenerator.Object,
                socketPatternDefinitionStatRepository.Object,
                socketPatternDefinitionEnchantmentRepository.Object);

            // Execute
            var result = converter.ConvertItem(
                itemContext.Object,
                randomizer.Object,
                itemToConvert.Object,
                socketPatternDefinition.Object);

            // Assert
            Assert.Equal(expectedResult.Object, result);

            baseItemEnchantments.Verify(x => x.GetEnumerator(), Times.Once);

            statMerger.Verify(x => x.MergeStats(It.IsAny<IEnumerable<IStat>>(), It.IsAny<IEnumerable<IStat>>()), Times.Once);

            itemMetaDataFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once);
            
            itemNamePartFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>()),
                Times.Once);
            
            statGenerator.Verify(
                x => x.GenerateStats(
                    It.IsAny<IRandom>(), 
                    It.IsAny<IEnumerable<IStatRange>>()),
                Times.Once);
            
            socketPatternDefinitionStatRepository.Verify(x => x.GetBySocketPatternId(It.IsAny<Guid>()), Times.Once);
            
            socketPatternDefinitionEnchantmentRepository.Verify(x => x.GetBySocketPatternId(It.IsAny<Guid>()), Times.Once);
            
            itemToConvert.Verify(x => x.InventoryGraphicResourceId, Times.Once);
            itemToConvert.Verify(x => x.MagicTypeId, Times.Once);
            itemToConvert.Verify(x => x.ItemTypeId, Times.Once);
            itemToConvert.Verify(x => x.MaterialTypeId, Times.Once);
            itemToConvert.Verify(x => x.SocketTypeId, Times.Once);
            itemToConvert.Verify(x => x.BaseStats, Times.Once);
            itemToConvert.Verify(x => x.Enchantments, Times.Once);
            itemToConvert.Verify(x => x.ItemDefinitionId, Times.Once);
            itemToConvert.Verify(x => x.Requirements, Times.Once);
            itemToConvert.Verify(x => x.Affixes, Times.Once);
            itemToConvert.Verify(x => x.SocketedItems, Times.Once);
            itemToConvert.Verify(x => x.EquippableSlotIds, Times.Once);

            socketPatternDefinition.Verify(x => x.InventoryGraphicResourceId, Times.Once);
            socketPatternDefinition.Verify(x => x.MagicTypeId, Times.Once);
            socketPatternDefinition.Verify(x => x.NameStringResourceId, Times.Once);
            socketPatternDefinition.Verify(x => x.Id, Times.Exactly(2));
            
            itemFactory.Verify(
                x => x.Create(
                    It.IsAny<IItemContext>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<IItemMetaData>(),
                    It.IsAny<IEnumerable<IItemNamePart>>(),
                    It.IsAny<IItemRequirements>(),
                    It.IsAny<IEnumerable<IStat>>(),
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IEnumerable<IItemAffix>>(),
                    It.IsAny<IEnumerable<IItem>>(),
                    It.IsAny<IEnumerable<Guid>>()),
                Times.Once);
        }

        [Fact]
        public void ConvertItem_WithOverrides_SocketPatternOverridesBaseItem()
        {
            // Setup
            var itemDefinitionId = Guid.NewGuid();
            var itemInventoryGraphicResourceId = Guid.NewGuid();
            var itemMagicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var itemMaterialTypeId = Guid.NewGuid();
            var itemSocketTypeId = Guid.NewGuid();
            var socketPatternNameStringResourceId = Guid.NewGuid();
            var socketPatternId = Guid.NewGuid();
            var socketPatternInventoryGraphicResourceId = Guid.NewGuid();
            var socketPatternMagicTypeId = Guid.NewGuid();
            var socketPatternEnchantmentDefinitionId = Guid.NewGuid();

            var baseItemStat = new Mock<IStat>(MockBehavior.Strict);
            var baseItemStats = new[]
            {
                baseItemStat.Object
            };

            var baseItemEnchantment = new Mock<IEnchantment>(MockBehavior.Strict);

            var baseItemEnchantments = new Mock<IEnchantmentCollection>(MockBehavior.Strict);
            baseItemEnchantments
                .Setup(x => x.GetEnumerator())
                .Returns(new List<IEnchantment>() { baseItemEnchantment.Object }.GetEnumerator());


            var randomizer = new Mock<IRandom>(MockBehavior.Strict);

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemMetaDataFactory = new Mock<IItemMetaDataFactory>(MockBehavior.Strict);
            itemMetaDataFactory
                .Setup(x => x.Create(
                    socketPatternInventoryGraphicResourceId,
                    socketPatternMagicTypeId,
                    itemTypeId,
                    itemMaterialTypeId,
                    itemSocketTypeId))
                .Returns(itemMetaData.Object);

            var itemNamePart = new Mock<IItemNamePart>(MockBehavior.Strict);

            var itemNamePartFactory = new Mock<IItemNamePartFactory>(MockBehavior.Strict);
            itemNamePartFactory
                .Setup(x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    socketPatternNameStringResourceId,
                    0))
                .Returns(itemNamePart.Object);

            var socketPatternStat = new Mock<ISocketPatternStat>(MockBehavior.Strict);
            var socketPatternStats = new[] { socketPatternStat.Object };

            var socketPatternGeneratedStats = new[]
            {
                new Mock<IStat>(MockBehavior.Strict).Object,
            };

            var mergedStats = baseItemStats.Concat(socketPatternGeneratedStats);

            var statMerger = new Mock<IStatMerger>(MockBehavior.Strict);
            statMerger
                .Setup(x => x.MergeStats(baseItemStats, socketPatternGeneratedStats))
                .Returns(mergedStats);

            var statGenerator = new Mock<IStatGenerator>(MockBehavior.Strict);
            statGenerator
                .Setup(x => x.GenerateStats(randomizer.Object, socketPatternStats))
                .Returns(socketPatternGeneratedStats);

            var socketPatternDefinitionStatRepository = new Mock<ISocketPatternDefinitionStatRepository>(MockBehavior.Strict);
            socketPatternDefinitionStatRepository
                .Setup(x => x.GetBySocketPatternId(socketPatternId))
                .Returns(socketPatternStats);

            var socketPatternEnchantment = new Mock<ISocketPatternEnchantment>(MockBehavior.Strict);
            socketPatternEnchantment
                .Setup(x => x.EnchantmentDefinitionId)
                .Returns(socketPatternEnchantmentDefinitionId);

            var generatesSocketPatternEnchantment = new Mock<IEnchantment>(MockBehavior.Strict);

            var enchantmentGenerator = new Mock<IEnchantmentGenerator>(MockBehavior.Strict);
            enchantmentGenerator
                .Setup(x => x.Generate(randomizer.Object, socketPatternEnchantmentDefinitionId))
                .Returns(generatesSocketPatternEnchantment.Object);

            var socketPatternDefinitionEnchantmentRepository = new Mock<ISocketPatternDefinitionEnchantmentRepository>(MockBehavior.Strict);
            socketPatternDefinitionEnchantmentRepository
                .Setup(x => x.GetBySocketPatternId(socketPatternId))
                .Returns(new[] { socketPatternEnchantment.Object });

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var itemContext = new Mock<IItemContext>(MockBehavior.Strict);

            var socketedItems = new Mock<IItemCollection>(MockBehavior.Strict);

            var itemToConvert = new Mock<IItem>(MockBehavior.Strict);
            itemToConvert
                .Setup(x => x.ItemTypeId)
                .Returns(itemTypeId);
            itemToConvert
                .Setup(x => x.MaterialTypeId)
                .Returns(itemMaterialTypeId);
            itemToConvert
                .Setup(x => x.SocketTypeId)
                .Returns(itemSocketTypeId);
            itemToConvert
                .Setup(x => x.BaseStats)
                .Returns(baseItemStats);
            itemToConvert
                .Setup(x => x.Enchantments)
                .Returns(baseItemEnchantments.Object);
            itemToConvert
                .Setup(x => x.ItemDefinitionId)
                .Returns(itemDefinitionId);
            itemToConvert
                .Setup(x => x.Requirements)
                .Returns(itemRequirements.Object);
            itemToConvert
                .Setup(x => x.Affixes)
                .Returns(Enumerable.Empty<IItemAffix>());
            itemToConvert
                .Setup(x => x.SocketedItems)
                .Returns(socketedItems.Object);
            itemToConvert
                .Setup(x => x.EquippableSlotIds)
                .Returns(Enumerable.Empty<Guid>());

            var socketPatternDefinition = new Mock<ISocketPatternDefinition>(MockBehavior.Strict);
            socketPatternDefinition
                .Setup(x => x.InventoryGraphicResourceId)
                .Returns(socketPatternInventoryGraphicResourceId);
            socketPatternDefinition
                .Setup(x => x.MagicTypeId)
                .Returns(socketPatternMagicTypeId);
            socketPatternDefinition
                .Setup(x => x.NameStringResourceId)
                .Returns(socketPatternNameStringResourceId);
            socketPatternDefinition
                .Setup(x => x.Id)
                .Returns(socketPatternId);

            var expectedResult = new Mock<IItem>(MockBehavior.Strict);

            var itemFactory = new Mock<IItemFactory>(MockBehavior.Strict);
            itemFactory
                .Setup(x => x.Create(
                    itemContext.Object,
                    It.IsAny<Guid>(),
                    itemDefinitionId,
                    itemMetaData.Object,
                    new[] {itemNamePart.Object},
                    itemRequirements.Object,
                    mergedStats,
                    It.Is<IEnumerable<IEnchantment>>(y => y.Count() == 2),
                    Enumerable.Empty<IItemAffix>(),
                    socketedItems.Object,
                    Enumerable.Empty<Guid>()))
                .Returns(expectedResult.Object);

            var converter = SocketPatternItemConverter.Create(
                statMerger.Object,
                itemFactory.Object,
                itemMetaDataFactory.Object,
                itemNamePartFactory.Object,
                statGenerator.Object,
                enchantmentGenerator.Object,
                socketPatternDefinitionStatRepository.Object,
                socketPatternDefinitionEnchantmentRepository.Object);

            // Execute
            var result = converter.ConvertItem(
                itemContext.Object,
                randomizer.Object,
                itemToConvert.Object,
                socketPatternDefinition.Object);

            // Assert
            Assert.Equal(expectedResult.Object, result);

            baseItemEnchantments.Verify(x => x.GetEnumerator(), Times.Once);

            itemMetaDataFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once);

            itemNamePartFactory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>()),
                Times.Once);

            statMerger.Verify(x => x.MergeStats(It.IsAny<IEnumerable<IStat>>(), It.IsAny<IEnumerable<IStat>>()), Times.Once);

            statGenerator.Verify(
                x => x.GenerateStats(
                    It.IsAny<IRandom>(),
                    It.IsAny<IEnumerable<IStatRange>>()),
                Times.Once);

            socketPatternDefinitionStatRepository.Verify(x => x.GetBySocketPatternId(It.IsAny<Guid>()), Times.Once);

            socketPatternEnchantment.Verify(x => x.EnchantmentDefinitionId, Times.Once);

            enchantmentGenerator.Verify(x => x.Generate(randomizer.Object, It.IsAny<Guid>()), Times.Once);

            socketPatternDefinitionEnchantmentRepository.Verify(x => x.GetBySocketPatternId(It.IsAny<Guid>()), Times.Once);
            
            itemToConvert.Verify(x => x.ItemTypeId, Times.Once);
            itemToConvert.Verify(x => x.MaterialTypeId, Times.Once);
            itemToConvert.Verify(x => x.SocketTypeId, Times.Once);
            itemToConvert.Verify(x => x.BaseStats, Times.Once);
            itemToConvert.Verify(x => x.Enchantments, Times.Once);
            itemToConvert.Verify(x => x.ItemDefinitionId, Times.Once);
            itemToConvert.Verify(x => x.Requirements, Times.Once);
            itemToConvert.Verify(x => x.Affixes, Times.Once);
            itemToConvert.Verify(x => x.SocketedItems, Times.Once);
            itemToConvert.Verify(x => x.EquippableSlotIds, Times.Once);

            socketPatternDefinition.Verify(x => x.InventoryGraphicResourceId, Times.Once);
            socketPatternDefinition.Verify(x => x.MagicTypeId, Times.Once);
            socketPatternDefinition.Verify(x => x.NameStringResourceId, Times.Once);
            socketPatternDefinition.Verify(x => x.Id, Times.Exactly(2));

            itemFactory.Verify(
                x => x.Create(
                    It.IsAny<IItemContext>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<IItemMetaData>(),
                    It.IsAny<IEnumerable<IItemNamePart>>(),
                    It.IsAny<IItemRequirements>(),
                    It.IsAny<IEnumerable<IStat>>(),
                    It.IsAny<IEnumerable<IEnchantment>>(),
                    It.IsAny<IEnumerable<IItemAffix>>(),
                    It.IsAny<IEnumerable<IItem>>(),
                    It.IsAny<IEnumerable<Guid>>()),
                Times.Once);
        }
        #endregion
    }
}
