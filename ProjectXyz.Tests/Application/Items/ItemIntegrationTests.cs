using System;

using Moq;
using Xunit;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Tests.Application.Items.Mocks;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Tests.Xunit.Assertions.Stats;
using ProjectXyz.Tests.Xunit.Assertions.Enchantments;
using ProjectXyz.Tests.Application.Enchantments.Mocks;

namespace ProjectXyz.Tests.Application.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemIntegrationTests
    {
        [Fact]
        public void Item_SaveAndLoad_CreatesEquivalentItem()
        {
            var sourceData = new Tests.Data.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.Value, 1234567))
                .WithEquippableSlots("Slot 1", "Slot 2")
                .Build();
            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    sourceData);
            var enchantment = new MockEnchantmentBuilder()
                .WithStatId(ActorStats.MaximumLife)
                .WithValue(1234567)
                .Build();
            item.Enchant(enchantment);

            var itemSaver = ItemSaver.Create(EnchantmentSaver.Create());
            var savedData = itemSaver.Save(item);

            var rebuiltItem = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    savedData);

            // metadata
            Assert.Equal(item.Id, rebuiltItem.Id);
            Assert.Equal(item.ItemType, rebuiltItem.ItemType);
            Assert.Equal(item.MagicType, rebuiltItem.MagicType);
            ////Assert.Equal(item.Material.MaterialType, rebuiltItem.Material.MaterialType);
            ////Assert.Equal(item.Material.Name, rebuiltItem.Material.Name);
            Assert.Equal(item.Name, rebuiltItem.Name);

            // slots
            Assert.Equal<string>(item.EquippableSlots, rebuiltItem.EquippableSlots);

            // stats
            AssertStats.Equal(item.Stats, rebuiltItem.Stats);

            // enchantments
            AssertEnchantments.Equal(item.Enchantments, rebuiltItem.Enchantments);
        }
    }
}
