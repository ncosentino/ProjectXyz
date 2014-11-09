using System;

using Moq;
using Xunit;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Data.Interface.Items.Materials;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Tests.Xunit.Categories;

namespace ProjectXyz.Tests.Application.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemTests
    {
        [Fact]
        public void Defaults()
        {
            var context = new Mock<IItemContext>();
            context
                .Setup(x => x.EnchantmentCalculator)
                .Returns(EnchantmentCalculator.Create());

            var item = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(context.Object, ProjectXyz.Data.Core.Items.Item.Create());
            Assert.NotNull(item.Id);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Equal(0, item.Durability.Maximum);
            Assert.Equal(0, item.Durability.Current);
            Assert.Equal(0, item.Weight);
            Assert.Equal(0, item.Value);
            Assert.Equal("Default", item.Name);
            Assert.Empty(item.Enchantments);
            Assert.Empty(item.SocketedItems);
            Assert.Empty(item.Requirements.Stats);
        }
    }
}
