using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Moq;

using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Core.Enchantments;
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
        public void AddToOpenSocketSumsWeight()
        {
            var socketCandidate = new Mock<IItem>();
            socketCandidate
                .Setup(x => x.RequiredSockets)
                .Returns(1);
            socketCandidate
                .Setup(x => x.Weight)
                .Returns(100);
            socketCandidate
                .Setup(x => x.Enchantments)
                .Returns(ProjectXyz.Application.Core.Enchantments.MutableEnchantmentCollection.Create());

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
        public void AddToOpenSocketAppliesEnchantments()
        {
            var socketCandidateEnchantment = new Mock<IEnchantment>();
            socketCandidateEnchantment
                .Setup(x => x.StatId)
                .Returns(ItemStats.Value);
            socketCandidateEnchantment
                .Setup(x => x.Value)
                .Returns(123456);
            socketCandidateEnchantment
                .Setup(x => x.CalculationId)
                .Returns(EnchantmentCalculationTypes.Value);

            var socketCandidate = new Mock<IItem>();
            socketCandidate
                .Setup(x => x.RequiredSockets)
                .Returns(1);
            socketCandidate
                .Setup(x => x.Enchantments)
                .Returns(ProjectXyz.Application.Core.Enchantments.MutableEnchantmentCollection.Create(new IEnchantment[]
                { 
                    socketCandidateEnchantment.Object 
                }));

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(MutableStat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Create(
                EnchantmentCalculator.Create(),
                socketableItemData);

            Assert.True(
                socketableItem.Socket(socketCandidate.Object),
                "Expecting the socket operation to be successful.");
            Assert.Contains(socketCandidate.Object, socketableItem.SocketedItems);
            Assert.Contains(socketCandidateEnchantment.Object, socketableItem.Enchantments);
            Assert.Equal(socketCandidateEnchantment.Object.Value, socketableItem.Value);
        }

        [Fact]
        public void SocketBadSocketCandidate()
        {
            var socketCandidate = new Mock<IItem>();
            socketCandidate
                .Setup(x => x.RequiredSockets)
                .Returns(0);
            socketCandidate
                .Setup(x => x.Enchantments)
                .Returns(ProjectXyz.Application.Core.Enchantments.MutableEnchantmentCollection.Create());

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
            socketCandidate
                .Setup(x => x.Enchantments)
                .Returns(ProjectXyz.Application.Core.Enchantments.MutableEnchantmentCollection.Create());

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

        [Fact]
        public void SocketEnchantmentsCanExpire()
        {
            var socketCandidateEnchantment = new Mock<IEnchantment>();
            socketCandidateEnchantment
                .Setup(x => x.StatId)
                .Returns(ItemStats.Value);
            socketCandidateEnchantment
                .Setup(x => x.Value)
                .Returns(123456);
            socketCandidateEnchantment
                .Setup(x => x.CalculationId)
                .Returns(EnchantmentCalculationTypes.Value);
            socketCandidateEnchantment
                .SetupSequence(x => x.RemainingDuration)
                .Returns(TimeSpan.FromSeconds(5))
                .Returns(TimeSpan.Zero);

            var socketCandidate = new Mock<IItem>();
            socketCandidate
                .Setup(x => x.RequiredSockets)
                .Returns(1);
            socketCandidate
                .Setup(x => x.Enchantments)
                .Returns(ProjectXyz.Application.Core.Enchantments.MutableEnchantmentCollection.Create(new IEnchantment[]
                { 
                    socketCandidateEnchantment.Object 
                }));

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(MutableStat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Create(
                EnchantmentCalculator.Create(),
                socketableItemData);

            Assert.True(
                socketableItem.Socket(socketCandidate.Object),
                "Expecting the socket operation to be successful.");
            Assert.Equal(socketCandidateEnchantment.Object.Value, socketableItem.Value);

            socketableItem.UpdateElapsedTime(socketCandidateEnchantment.Object.RemainingDuration);
            Assert.DoesNotContain(socketCandidateEnchantment.Object, socketableItem.Enchantments);
            Assert.Equal(0, socketableItem.Value);
        }
    }
}
