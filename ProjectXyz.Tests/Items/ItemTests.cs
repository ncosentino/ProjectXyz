using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;

using ProjectXyz.Interface.Enchantments;
using ProjectXyz.Interface.Stats;
using ProjectXyz.Interface.Items;
using ProjectXyz.Core.Items;
using ProjectXyz.Core.Enchantments;
using ProjectXyz.Core.Stats;

namespace ProjectXyz.Tests.Items
{
    public class ItemTests
    {
        [Fact]
        public void Defaults()
        {
            var item = Item.Create(EnchantmentCalculator.Create());
            Assert.NotNull(item.Id);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Equal(0, item.Durability.Maximum);
            Assert.Equal(0, item.Durability.Current);
            Assert.Equal(0, item.Weight);
            Assert.Equal(0, item.Value);
            Assert.Equal(string.Empty, item.Name);
            Assert.Empty(item.Enchantments);
            Assert.Empty(item.Sockets.Items);
            Assert.Empty(item.Requirements.Stats);
        }

        [Fact]
        public void EnchantItemMaximumDurability()
        {
            var item = Item.Create(EnchantmentCalculator.Create());
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
