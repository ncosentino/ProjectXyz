using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Tests.Items
{
    public class ItemEnchantingTests
    {
        [Fact]
        public void EnchantItemMaximumDurability()
        {
            var item = Item.Create(
                EnchantmentCalculator.Create(),
                ProjectXyz.Data.Core.Items.Item.Create());
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
