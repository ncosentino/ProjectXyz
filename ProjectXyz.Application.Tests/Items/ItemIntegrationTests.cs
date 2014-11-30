using Moq;

using Xunit;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Tests.Xunit.Assertions.Enchantments;
using ProjectXyz.Application.Tests.Xunit.Assertions.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Core.Enchantments;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemIntegrationTests
    {
        [Fact]
        public void Item_SaveAndLoad_CreatesEquivalentItem()
        {
            var sourceData = new Data.Tests.Items.Mocks.MockItemBuilder()
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

            var itemSaver = ItemSaver.Create(EnchantmentSaver.Create(EnchantmentStoreFactory.Create()));
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
            Assert.Equal(item.MagicTypeId, rebuiltItem.MagicTypeId);
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
