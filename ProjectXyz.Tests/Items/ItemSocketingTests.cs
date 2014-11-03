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
    public class ItemSocketingTests
    {
        [Fact]
        public void AddToOpenSocket()
        {
            var socketCandidate = new Mock<IItem>();
            socketCandidate
                .Setup(x => x.RequiredSockets)
                .Returns(1);
            socketCandidate
                .Setup(x => x.Weight)
                .Returns(100);

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(MutableStat.Create(ItemStats.TotalSockets, 1));
            socketableItemData.Stats.Set(MutableStat.Create(ItemStats.Weight, 50));
            var socketableItem = Item.Create(
                EnchantmentCalculator.Create(),
                socketableItemData);

            Assert.True(
                socketableItem.Socket(socketCandidate.Object),
                "Expecting the socket operation to be successful.");
            Assert.Contains(socketCandidate.Object, socketableItem.SocketedItems);
            Assert.Equal(
                socketableItem.TotalSockets - socketCandidate.Object.RequiredSockets,
                socketableItem.OpenSockets);
            Assert.Equal(150, socketableItem.Weight);
        }

        [Fact]
        public void SocketBadSocketCandidate()
        {
            var socketCandidate = new Mock<IItem>();
            socketCandidate
                .Setup(x => x.RequiredSockets)
                .Returns(0);

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(MutableStat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Create(
                EnchantmentCalculator.Create(),
                socketableItemData);

            Assert.False(
                socketableItem.Socket(socketCandidate.Object),
                "Expecting the socket operation to be successful.");
            Assert.DoesNotContain(socketCandidate.Object, socketableItem.SocketedItems);
        }

        [Fact]
        public void AddNoOpenSockets()
        {
            var socketCandidate = new Mock<IItem>();
            socketCandidate
                .Setup(x => x.RequiredSockets)
                .Returns(1);

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(MutableStat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Create(
                EnchantmentCalculator.Create(),
                socketableItemData);

            Assert.True(
                socketableItem.Socket(socketCandidate.Object),
                "Expecting the socket operation to be successful.");
            Assert.Equal(0, socketableItem.OpenSockets);
            Assert.False(
                socketableItem.Socket(socketCandidate.Object),
                "Expecting the second socket operation to fail.");
        }
    }
}
