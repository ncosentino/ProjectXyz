using System;

using Moq;
using Xunit;

using ProjectXyz.Application.Core.Enchantments;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Tests.Application.Items
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
                .Returns(ProjectXyz.Application.Core.Enchantments.EnchantmentCollection.Create());

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(Stat.Create(ItemStats.TotalSockets, 1));
            socketableItemData.Stats.Set(Stat.Create(ItemStats.Weight, 50));
            var socketableItem = Item.Builder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(EnchantmentCalculator.Create(), socketableItemData);

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
                .Returns(ProjectXyz.Application.Core.Enchantments.EnchantmentCollection.Create(new IEnchantment[]
                { 
                    socketCandidateEnchantment.Object 
                }));

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(Stat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Builder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(EnchantmentCalculator.Create(), socketableItemData);

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
                .Returns(ProjectXyz.Application.Core.Enchantments.EnchantmentCollection.Create());

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(Stat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Builder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(EnchantmentCalculator.Create(), socketableItemData);

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
                .Returns(ProjectXyz.Application.Core.Enchantments.EnchantmentCollection.Create());

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(Stat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Builder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(EnchantmentCalculator.Create(), socketableItemData);

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
                .Returns(ProjectXyz.Application.Core.Enchantments.EnchantmentCollection.Create(new IEnchantment[]
                { 
                    socketCandidateEnchantment.Object 
                }));

            var socketableItemData = ProjectXyz.Data.Core.Items.Item.Create();
            socketableItemData.Stats.Set(Stat.Create(ItemStats.TotalSockets, 1));
            var socketableItem = Item.Builder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(EnchantmentCalculator.Create(), socketableItemData);

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
