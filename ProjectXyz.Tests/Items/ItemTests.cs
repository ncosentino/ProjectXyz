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
    public class ItemTests
    {
        [Fact]
        public void Defaults()
        {
            var item = Item.Create(
                EnchantmentCalculator.Create(),
                ProjectXyz.Data.Core.Items.Item.Create());
            Assert.NotNull(item.Id);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Equal(0, item.Durability.Maximum);
            Assert.Equal(0, item.Durability.Current);
            Assert.Equal(0, item.Weight);
            Assert.Equal(0, item.Value);
            Assert.Equal(string.Empty, item.Name);
            Assert.Empty(item.Enchantments);
            Assert.Empty(item.SocketedItems);
            Assert.Empty(item.Requirements.Stats);
        }
    }
}
