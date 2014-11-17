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
using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Tests.Application.Items.Mocks;
using ProjectXyz.Tests.Application.Enchantments.Mocks;

namespace ProjectXyz.Tests.Application.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemSocketingTests
    {
        [Fact]
        public void Item_AddToOpenSocket_SumsWeight()
        {
            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(1)
                .WithWeight(100)
                .Build();

            var socketableItemData = new ProjectXyz.Tests.Data.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(ItemStats.TotalSockets, 1),
                    Stat.Create(ItemStats.Weight, 50))
                .Build();

            var socketableItem = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    socketableItemData);

            Assert.True(
                socketableItem.Socket(socketCandidate),
                "Expecting the socket operation to be successful.");
            Assert.Contains(socketCandidate, socketableItem.SocketedItems);
            Assert.Equal(
                socketableItem.TotalSockets - socketCandidate.RequiredSockets,
                socketableItem.OpenSockets);
            Assert.Equal(150, socketableItem.Weight);
        }

        [Fact]
        public void Item_AddToOpenSocket_AppliesEnchantments()
        {
            var socketCandidateEnchantment = new MockEnchantmentBuilder()
                .WithStatId(ItemStats.Value)
                .WithValue(123456)
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .Build();

            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(1)
                .WithEnchantments(socketCandidateEnchantment)
                .Build();

            var socketableItemData = new ProjectXyz.Tests.Data.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.TotalSockets, 1))
                .Build();

            var socketableItem = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    socketableItemData);

            Assert.True(
                socketableItem.Socket(socketCandidate),
                "Expecting the socket operation to be successful.");
            Assert.Contains(socketCandidate, socketableItem.SocketedItems);
            Assert.Contains(socketCandidateEnchantment, socketableItem.Enchantments);
            Assert.Equal(socketCandidateEnchantment.Value, socketableItem.Value);
        }

        [Fact]
        public void Item_AddNoSocketsRequiredCandidateToOpenSocket_FailsToSocket()
        {
            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(0)
                .Build();

            var socketableItemData = new ProjectXyz.Tests.Data.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.TotalSockets, 1))
                .Build();

            var socketableItem = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    socketableItemData);

            Assert.False(
                socketableItem.Socket(socketCandidate),
                "Expecting the socket operation to be successful.");
            Assert.DoesNotContain(socketCandidate, socketableItem.SocketedItems);
        }

        [Fact]
        public void Item_TrySocketWithNoOpenSockets_FailsToSocket()
        {
            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(1)
                .Build();

            var socketableItemData = new ProjectXyz.Tests.Data.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.TotalSockets, 0))
                .Build();

            var socketableItem = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    socketableItemData);

            Assert.Equal(0, socketableItem.OpenSockets);
            Assert.False(
                socketableItem.Socket(socketCandidate),
                "Expecting the second socket operation to fail.");
        }

        [Fact]
        public void Item_SocketedItemEnchantments_CanExpire()
        {
            var socketCandidateEnchantment = new MockEnchantmentBuilder()
                .WithStatId(ItemStats.Value)
                .WithValue(123456)
                .WithCalculationId(EnchantmentCalculationTypes.Value)
                .WithRemainingTime(TimeSpan.FromSeconds(5), TimeSpan.Zero)
                .Build();

            var socketCandidate = new MockItemBuilder()
                .WithEnchantments(socketCandidateEnchantment)
                .WithRequiredSockets(1)
                .Build();

            var socketableItemData = new ProjectXyz.Tests.Data.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(ItemStats.TotalSockets, 1))
                .Build();
            var socketableItem = ItemBuilder
                .Create()
                .WithMaterialFactory(new Mock<IMaterialFactory>().Object)
                .Build(
                    new MockItemContextBuilder().Build(),
                    socketableItemData);

            Assert.True(
                socketableItem.Socket(socketCandidate),
                "Expecting the socket operation to be successful.");
            Assert.Equal(socketCandidateEnchantment.Value, socketableItem.Value);

            socketableItem.UpdateElapsedTime(socketCandidateEnchantment.RemainingDuration);
            Assert.DoesNotContain(socketCandidateEnchantment, socketableItem.Enchantments);
            Assert.Equal(0, socketableItem.Value);
        }
    }
}
