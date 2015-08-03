using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using Moq;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Tests.Xunit.Assertions.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemSaverTests
    {
        [Fact]
        public void ItemSaver_Save_MetadataMatches()
        {
            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder().Build();
            var item = Item.Create(
                new MockItemContextBuilder().Build(),
                sourceData);

            var itemSaver = ItemSaver.Create(new MockEnchantmentSaverBuilder().Build());
            var savedData = itemSaver.Save(item);

            Assert.Equal(sourceData.Id, savedData.Id);
            Assert.Equal(sourceData.ItemType, savedData.ItemType);
            Assert.Equal(sourceData.MagicTypeId, savedData.MagicTypeId);
            Assert.Equal(sourceData.MaterialTypeId, savedData.MaterialTypeId);
            Assert.Equal(sourceData.Name, savedData.Name);
        }

        [Fact]
        public void ItemSaver_Save_EquippableSlotsMatch()
        {
            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithEquippableSlots("Slot 1", "Slot 2")
                .Build();

            var item = Item.Create(
                new MockItemContextBuilder().Build(),
                sourceData);

            var itemSaver = ItemSaver.Create(new MockEnchantmentSaverBuilder().Build());
            var savedData = itemSaver.Save(item);

            Assert.Equal<string>(sourceData.EquippableSlots, savedData.EquippableSlots);
        }

        [Fact]
        public void ItemSaver_Save_StatsMatch()
        {
            // Setup
            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.Value, 1234567))
                .Build();

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>();
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(new[]
                {
                    StatSocketType.Create(Guid.NewGuid(), Guid.NewGuid()), 
                });

            var item = Item.Create(
                new MockItemContextBuilder().WithStatSocketTypeRepository(statSocketTypeRepository.Object).Build(),
                sourceData);

            var itemSaver = ItemSaver.Create(new MockEnchantmentSaverBuilder().Build());

            // Execute
            var savedData = itemSaver.Save(item);

            // Assert
            Assert.Equal(5, savedData.Stats.Count);
        }

        [Fact]
        public void ItemSaver_Save_EnchantmentsMatch()
        {
            var enchantment = new MockEnchantmentBuilder()
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(1234567)
                .Build();

            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .Build();
            var item = Item.Create(
                new MockItemContextBuilder().Build(),
                sourceData);
            item.Enchant(enchantment);

            var itemSaver = ItemSaver.Create(EnchantmentSaver.Create(EnchantmentStoreFactory.Create()));
            var savedData = itemSaver.Save(item);

            Assert.Equal(1, savedData.Enchantments.Count);
            Assert.Equal(enchantment.StatId, savedData.Enchantments[0].StatId);
            Assert.Equal(enchantment.Value, savedData.Enchantments[0].Value);
            Assert.Equal(enchantment.CalculationId, savedData.Enchantments[0].CalculationId);
            Assert.Equal(enchantment.RemainingDuration, savedData.Enchantments[0].RemainingDuration);
        }

        [Fact]
        public void ItemSaver_Save_SocketedItemsMatch()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();

            var socketCandidateData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.RequiredSockets, 1))
                .WithSocketTypeId(socketTypeId)
                .Build();
            var socketCandidate = Item.Create(
                new MockItemContextBuilder().Build(),
                socketCandidateData);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>();
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(StatSocketType.Create(statIdForSocketing, socketTypeId));

            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(statIdForSocketing, 1))
                .Build();
            var item = Item.Create(
                new MockItemContextBuilder().WithStatSocketTypeRepository(statSocketTypeRepository.Object).Build(),
                sourceData);
            Assert.True(
                item.Socket(socketCandidate),
                "Expecting to socket the item.");

            var itemSaver = ItemSaver.Create(EnchantmentSaver.Create(EnchantmentStoreFactory.Create()));

            // Execute
            var savedData = itemSaver.Save(item);
          
            // Assert
            Assert.Equal(1, savedData.SocketedItems.Count);
          
            var actualSocketedItem = new List<ProjectXyz.Data.Interface.Items.IItemStore>(savedData.SocketedItems)[0];
            Assert.Equal(socketCandidate.Id, actualSocketedItem.Id);
        }

        [Fact]
        public void ItemSaver_Save_RequirementsMatch()
        {
            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder()
                .Build();
            var item = Item.Create(
                new MockItemContextBuilder().Build(),
                sourceData);

            var itemSaver = ItemSaver.Create(EnchantmentSaver.Create(EnchantmentStoreFactory.Create()));
            var savedData = itemSaver.Save(item);

            Assert.Equal(sourceData.Requirements.Class, savedData.Requirements.Class);
            Assert.Equal(sourceData.Requirements.Level, savedData.Requirements.Level);
            Assert.Equal(sourceData.Requirements.Race, savedData.Requirements.Race);
            AssertStats.Equal(sourceData.Requirements.Stats, savedData.Requirements.Stats);
        }
    }
}
