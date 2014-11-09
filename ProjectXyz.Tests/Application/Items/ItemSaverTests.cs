using Moq;
using Xunit;

using ProjectXyz.Application.Core.Items;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Tests.Application.Items.Mocks;

namespace ProjectXyz.Tests.Application.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemSaverTests
    {
        [Fact]
        public void SaveMetadata()
        {
            var sourceData = new Tests.Data.Items.Mocks.MockItemBuilder().Build();
            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    sourceData);

            var itemSaver = ItemSaver.Create();
            var savedData = itemSaver.Save(item);

            Assert.Equal(sourceData.Id, savedData.Id);
            Assert.Equal(sourceData.ItemType, savedData.ItemType);
            Assert.Equal(sourceData.MagicType, savedData.MagicType);
            Assert.Equal(sourceData.MaterialType, savedData.MaterialType);
            Assert.Equal(sourceData.Name, savedData.Name);
        }

        [Fact]
        public void SaveEquippableSlots()
        {
            var sourceData = new Tests.Data.Items.Mocks.MockItemBuilder()
                .WithEquippableSlots("Slot 1", "Slot 2")
                .Build();

            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    sourceData);

            var itemSaver = ItemSaver.Create();
            var savedData = itemSaver.Save(item);

            Assert.Equal<string>(sourceData.EquippableSlots, savedData.EquippableSlots);
        }

        [Fact]
        public void SaveStats()
        {
            var sourceData = new Tests.Data.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.Value, 1234567))
                .Build();

            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    sourceData);

            var itemSaver = ItemSaver.Create();
            var savedData = itemSaver.Save(item);

            Assert.Equal(5, savedData.Stats.Count);
        }
    }
}
