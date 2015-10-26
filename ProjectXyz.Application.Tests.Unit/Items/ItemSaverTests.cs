using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Requirements;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemSaverTests
    {
        #region Methods

        [Fact]
        public void Save_BasicItem_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();
            var raceDefinitionId = Guid.NewGuid();
            var classDefinitionId = Guid.NewGuid();
            var itemMiscRequirementsId = Guid.NewGuid();
            var itemNamePartId = Guid.NewGuid();
            var itemNamePartPartId = Guid.NewGuid();
            var itemNamePartNameStringResourceId = Guid.NewGuid();

            var itemStore = new Mock<IItemStore>(MockBehavior.Strict);

            var itemStats = new Mock<IStatCollection>(MockBehavior.Strict);
            itemStats
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IStat>().GetEnumerator);

            var itemEnchantments = new Mock<IEnchantmentCollection>(MockBehavior.Strict);
            itemEnchantments
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IEnchantment>().GetEnumerator);

            var itemSocketedItems = new Mock<IItemCollection>(MockBehavior.Strict);
            itemSocketedItems
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IItem>().GetEnumerator);

            var itemRequirementsStats = new Mock<IStatCollection>(MockBehavior.Strict);
            itemRequirementsStats
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IStat>().GetEnumerator);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);
            itemRequirements
                .Setup(x => x.Stats)
                .Returns(itemRequirementsStats.Object);
            itemRequirements
                .Setup(x => x.RaceDefinitionId)
                .Returns(raceDefinitionId);
            itemRequirements
                .Setup(x => x.ClassDefinitionId)
                .Returns(classDefinitionId);

            var itemNamePart = new Mock<IItemNamePart>(MockBehavior.Strict);
            itemNamePart
                .Setup(x => x.Id)
                .Returns(itemNamePartId);
            itemNamePart
                .Setup(x => x.PartId)
                .Returns(itemNamePartPartId);
            itemNamePart
                .Setup(x => x.NameStringResourceId)
                .Returns(itemNamePartNameStringResourceId);
            itemNamePart
                .Setup(x => x.Order)
                .Returns(1);

            var itemNameParts = new[]
            {
                itemNamePart.Object,
            };

            var sourceItem = new Mock<IItem>(MockBehavior.Strict);
            sourceItem
                .Setup(x => x.Id)
                .Returns(id);
            sourceItem
                .Setup(x => x.ItemDefinitionId)
                .Returns(itemDefinitionId);
            sourceItem
                .Setup(x => x.ItemNameParts)
                .Returns(itemNameParts);
            sourceItem
                .Setup(x => x.InventoryGraphicResourceId)
                .Returns(inventoryGraphicResourceId);
            sourceItem
                .Setup(x => x.MagicTypeId)
                .Returns(magicTypeId);
            sourceItem
                .Setup(x => x.ItemTypeId)
                .Returns(itemTypeId);
            sourceItem
                .Setup(x => x.MaterialTypeId)
                .Returns(materialTypeId);
            sourceItem
                .Setup(x => x.SocketTypeId)
                .Returns(socketTypeId);
            sourceItem
                .Setup(x => x.Stats)
                .Returns(itemStats.Object);
            sourceItem
                .Setup(x => x.Enchantments)
                .Returns(itemEnchantments.Object);
            sourceItem
                .Setup(x => x.SocketedItems)
                .Returns(itemSocketedItems.Object);
            sourceItem
                .Setup(x => x.Requirements)
                .Returns(itemRequirements.Object);

            var enchantmentSaver = new Mock<IEnchantmentSaver>(MockBehavior.Strict);

            var itemEnchantmentRepository = new Mock<IItemEnchantmentRepository>(MockBehavior.Strict);

            var itemStoreRepository = new Mock<IItemStoreRepository>(MockBehavior.Strict);
            itemStoreRepository
                .Setup(x => x.Add(
                    id,
                    itemDefinitionId,
                    itemNamePartPartId,
                    inventoryGraphicResourceId,
                    magicTypeId,
                    itemTypeId,
                    materialTypeId,
                    socketTypeId))
                .Returns(itemStore.Object);

            var itemMiscRequirements = new Mock<IItemMiscRequirements>(MockBehavior.Strict);
            itemMiscRequirements
                .Setup(x => x.Id)
                .Returns(itemMiscRequirementsId);

            var itemMiscRequirementsRepository = new Mock<IItemMiscRequirementsRepository>(MockBehavior.Strict);
            itemMiscRequirementsRepository
                .Setup(x => x.Add(It.IsAny<Guid>(), raceDefinitionId, classDefinitionId))
                .Returns(itemMiscRequirements.Object);

            var itemStoreItemMiscRequirements = new Mock<IItemStoreItemMiscRequirements>(MockBehavior.Strict);

            var itemStoreItemMiscRequirementsRepository = new Mock<IItemStoreItemMiscRequirementsRepository>(MockBehavior.Strict);
            itemStoreItemMiscRequirementsRepository
                .Setup(x => x.Add(It.IsAny<Guid>(), id, itemMiscRequirementsId))
                .Returns(itemStoreItemMiscRequirements.Object);

            var itemStatRequirementsRepository = new Mock<IItemStatRequirementsRepository>(MockBehavior.Strict);

            var socketedItemRepository = new Mock<ISocketedItemRepository>(MockBehavior.Strict);

            var itemStatRepository = new Mock<IItemStatRepository>(MockBehavior.Strict);

            var statRepository = new Mock<IStatRepository>(MockBehavior.Strict);

            var itemNamePartRepository = new Mock<IItemNamePartRepository>(MockBehavior.Strict);
            itemNamePartRepository
                .Setup(x => x.RemoveByPartId(itemNamePartPartId));
            itemNamePartRepository
                .Setup(x => x.Add(
                    itemNamePartId,
                    itemNamePartPartId,
                    itemNamePartNameStringResourceId,
                    1))
                .Returns(itemNamePart.Object);

            var itemSaver = ItemSaver.Create(
               enchantmentSaver.Object,
               itemEnchantmentRepository.Object,
               itemStoreRepository.Object,
               itemStoreItemMiscRequirementsRepository.Object,
               itemMiscRequirementsRepository.Object,
               itemStatRequirementsRepository.Object,
               socketedItemRepository.Object,
               itemStatRepository.Object,
               statRepository.Object,
               itemNamePartRepository.Object);
            
            // Execute
            var result = itemSaver.Save(sourceItem.Object);

            // Assert
            Assert.Equal(itemStore.Object, result);

            itemStats.Verify(x => x.GetEnumerator(), Times.Once());

            itemEnchantments.Verify(x => x.GetEnumerator(), Times.Once());

            itemSocketedItems.Verify(x => x.GetEnumerator(), Times.Once());

            itemRequirementsStats.Verify(x => x.GetEnumerator(), Times.Once());

            itemRequirements.Verify(x => x.Stats);
            itemRequirements.Verify(x => x.RaceDefinitionId);
            itemRequirements.Verify(x => x.ClassDefinitionId);

            itemNamePart.Verify(x => x.Id, Times.Once());
            itemNamePart.Verify(x => x.PartId, Times.AtLeastOnce);
            itemNamePart.Verify(x => x.NameStringResourceId, Times.Once());
            itemNamePart.Verify(x => x.Order, Times.Once());

            sourceItem.Verify(x => x.Id, Times.Exactly(2));
            sourceItem.Verify(x => x.ItemDefinitionId, Times.Once());
            sourceItem.Verify(x => x.ItemNameParts, Times.AtLeastOnce());
            sourceItem.Verify(x => x.InventoryGraphicResourceId, Times.Once());
            sourceItem.Verify(x => x.MagicTypeId, Times.Once());
            sourceItem.Verify(x => x.ItemTypeId, Times.Once());
            sourceItem.Verify(x => x.MaterialTypeId, Times.Once());
            sourceItem.Verify(x => x.SocketTypeId, Times.Once());
            sourceItem.Verify(x => x.Stats, Times.Once());
            sourceItem.Verify(x => x.Enchantments, Times.Once());
            sourceItem.Verify(x => x.SocketedItems, Times.Once());
            sourceItem.Verify(x => x.Requirements, Times.Once());

            itemStoreRepository.Verify(
                x => x.Add(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once());

            itemMiscRequirementsRepository.Verify(
                x => x.Add(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()), 
                Times.Once());

            itemMiscRequirements.Verify(x => x.Id, Times.Once());

            itemStoreItemMiscRequirementsRepository.Verify(x => x.Add(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());

            itemNamePartRepository.Verify(x => x.RemoveByPartId(It.IsAny<Guid>()), Times.Once);
            itemNamePartRepository.Verify(
                x => x.Add(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>()),
                Times.Once);
        }

        [Fact]
        public void Save_HasStats_Success()
        {
            // Setup
            var id = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var inventoryGraphicResourceId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var itemTypeId = Guid.NewGuid();
            var materialTypeId = Guid.NewGuid();
            var socketTypeId = Guid.NewGuid();
            var raceDefinitionId = Guid.NewGuid();
            var classDefinitionId = Guid.NewGuid();
            var statId = Guid.NewGuid();
            var statDefinitionId = Guid.NewGuid();
            var itemMiscRequirementsId = Guid.NewGuid();
            var itemNamePartId = Guid.NewGuid();
            var itemNamePartPartId = Guid.NewGuid();
            var itemNamePartNameStringResourceId = Guid.NewGuid();
            const int STAT_VALUE = 123;

            var itemStore = new Mock<IItemStore>(MockBehavior.Strict);

            var itemStat = new Mock<IStat>(MockBehavior.Strict);
            itemStat
                .Setup(x => x.Id)
                .Returns(statId);
            itemStat
                .Setup(x => x.StatDefinitionId)
                .Returns(statDefinitionId);
            itemStat
                .Setup(x => x.Value)
                .Returns(STAT_VALUE);

            var itemStats = new Mock<IStatCollection>(MockBehavior.Strict);
            itemStats
                .Setup(x => x.GetEnumerator())
                .Returns(new List<IStat>()
                {
                    itemStat.Object
                }.GetEnumerator());

            var itemEnchantments = new Mock<IEnchantmentCollection>(MockBehavior.Strict);
            itemEnchantments
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IEnchantment>().GetEnumerator);

            var itemSocketedItems = new Mock<IItemCollection>(MockBehavior.Strict);
            itemSocketedItems
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IItem>().GetEnumerator);

            var itemRequirementsStats = new Mock<IStatCollection>(MockBehavior.Strict);
            itemRequirementsStats
                .Setup(x => x.GetEnumerator())
                .Returns(Enumerable.Empty<IStat>().GetEnumerator);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);
            itemRequirements
                .Setup(x => x.Stats)
                .Returns(itemRequirementsStats.Object);
            itemRequirements
                .Setup(x => x.RaceDefinitionId)
                .Returns(raceDefinitionId);
            itemRequirements
                .Setup(x => x.ClassDefinitionId)
                .Returns(classDefinitionId);

            var itemNamePart = new Mock<IItemNamePart>(MockBehavior.Strict);
            itemNamePart
                .Setup(x => x.Id)
                .Returns(itemNamePartId);
            itemNamePart
                .Setup(x => x.PartId)
                .Returns(itemNamePartPartId);
            itemNamePart
                .Setup(x => x.NameStringResourceId)
                .Returns(itemNamePartNameStringResourceId);
            itemNamePart
                .Setup(x => x.Order)
                .Returns(1);

            var itemNameParts = new[]
            {
                itemNamePart.Object,
            };

            var sourceItem = new Mock<IItem>(MockBehavior.Strict);
            sourceItem
                .Setup(x => x.Id)
                .Returns(id);
            sourceItem
                .Setup(x => x.ItemDefinitionId)
                .Returns(itemDefinitionId);
            sourceItem
                .Setup(x => x.ItemNameParts)
                .Returns(itemNameParts);
            sourceItem
                .Setup(x => x.InventoryGraphicResourceId)
                .Returns(inventoryGraphicResourceId);
            sourceItem
                .Setup(x => x.MagicTypeId)
                .Returns(magicTypeId);
            sourceItem
                .Setup(x => x.ItemTypeId)
                .Returns(itemTypeId);
            sourceItem
                .Setup(x => x.MaterialTypeId)
                .Returns(materialTypeId);
            sourceItem
                .Setup(x => x.SocketTypeId)
                .Returns(socketTypeId);
            sourceItem
                .Setup(x => x.Stats)
                .Returns(itemStats.Object);
            sourceItem
                .Setup(x => x.Enchantments)
                .Returns(itemEnchantments.Object);
            sourceItem
                .Setup(x => x.SocketedItems)
                .Returns(itemSocketedItems.Object);
            sourceItem
                .Setup(x => x.Requirements)
                .Returns(itemRequirements.Object);

            var enchantmentSaver = new Mock<IEnchantmentSaver>(MockBehavior.Strict);

            var itemEnchantmentRepository = new Mock<IItemEnchantmentRepository>(MockBehavior.Strict);

            var itemStoreRepository = new Mock<IItemStoreRepository>(MockBehavior.Strict);
            itemStoreRepository
                .Setup(x => x.Add(
                    id,
                    itemDefinitionId,
                    itemNamePartPartId,
                    inventoryGraphicResourceId,
                    magicTypeId,
                    itemTypeId,
                    materialTypeId,
                    socketTypeId))
                .Returns(itemStore.Object);

            var itemMiscRequirements = new Mock<IItemMiscRequirements>(MockBehavior.Strict);
            itemMiscRequirements
                .Setup(x => x.Id)
                .Returns(itemMiscRequirementsId);

            var itemStoreItemMiscRequirements = new Mock<IItemStoreItemMiscRequirements>(MockBehavior.Strict);

            var itemStoreItemMiscRequirementsRepository =
                new Mock<IItemStoreItemMiscRequirementsRepository>(MockBehavior.Strict);
            itemStoreItemMiscRequirementsRepository
                .Setup(x => x.Add(It.IsAny<Guid>(), id, itemMiscRequirementsId))
                .Returns(itemStoreItemMiscRequirements.Object);

            var itemMiscRequirementsRepository = new Mock<IItemMiscRequirementsRepository>(MockBehavior.Strict);
            itemMiscRequirementsRepository
                .Setup(x => x.Add(It.IsAny<Guid>(), raceDefinitionId, classDefinitionId))
                .Returns(itemMiscRequirements.Object);

            var itemStatRequirementsRepository = new Mock<IItemStatRequirementsRepository>(MockBehavior.Strict);

            var socketedItemRepository = new Mock<ISocketedItemRepository>(MockBehavior.Strict);

            var itemStatRepository = new Mock<IItemStatRepository>(MockBehavior.Strict);
            itemStatRepository
                .Setup(x => x.Add(
                    It.IsAny<Guid>(),
                    id,
                    statId))
                .Returns(new Mock<IItemStat>(MockBehavior.Strict).Object);

            var statRepository = new Mock<IStatRepository>(MockBehavior.Strict);
            statRepository
                .Setup(x => x.Add(
                    statId,
                    statDefinitionId,
                    STAT_VALUE))
                .Returns(itemStat.Object);

            var itemNamePartRepository = new Mock<IItemNamePartRepository>(MockBehavior.Strict);
            itemNamePartRepository
                .Setup(x => x.RemoveByPartId(itemNamePartPartId));
            itemNamePartRepository
                .Setup(x => x.Add(
                    itemNamePartId,
                    itemNamePartPartId,
                    itemNamePartNameStringResourceId,
                    1))
                .Returns(itemNamePart.Object);

            var itemSaver = ItemSaver.Create(
                enchantmentSaver.Object,
                itemEnchantmentRepository.Object,
                itemStoreRepository.Object,
                itemStoreItemMiscRequirementsRepository.Object,
                itemMiscRequirementsRepository.Object,
                itemStatRequirementsRepository.Object,
                socketedItemRepository.Object,
                itemStatRepository.Object,
                statRepository.Object,
                itemNamePartRepository.Object);

            // Execute
            var result = itemSaver.Save(sourceItem.Object);

            // Assert
            Assert.Equal(itemStore.Object, result);

            itemStats.Verify(x => x.GetEnumerator(), Times.Once());

            itemStat.Verify(x => x.Id, Times.Once());
            itemStat.Verify(x => x.Value, Times.Once());

            itemStatRepository.Verify(
                x => x.Add(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once);

            statRepository.Verify(
                x => x.Add(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<double>()),
                Times.Once);

            itemNamePartRepository.Verify(x => x.RemoveByPartId(It.IsAny<Guid>()), Times.Once);
            itemNamePartRepository.Verify(
                x => x.Add(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>()),
                Times.Once);
        }

        #endregion
    }
}
