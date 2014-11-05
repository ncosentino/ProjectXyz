using System;

using Moq;
using Xunit;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Tests.Application.Items
{
    public class ItemEnchantingTests
    {
        [Fact]
        public void EnchantItemMaximumDurability()
        {
            var item = Item.Builder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(EnchantmentCalculator.Create(), ProjectXyz.Data.Core.Items.Item.Create());
            var baseDurability = ReadonlyDurability.Clone(item.Durability);
            var enchantment = new Mock<IEnchantment>();
            enchantment
                .Setup(x => x.CalculationId)
                .Returns("Value");
            enchantment
                .Setup(x => x.StatId)
                .Returns(ItemStats.MaximumDurability);
            enchantment
                .Setup(x => x.Value)
                .Returns(100);

            item.Enchant(enchantment.Object);

            Assert.Equal(baseDurability.Current, item.Durability.Current);
            Assert.Equal(baseDurability.Maximum + 100, item.Durability.Maximum);
        }
    }
}
