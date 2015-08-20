﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Tests.Enchantments.Mocks;
using ProjectXyz.Application.Tests.Items.Mocks;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Plugins.Enchantments.Additive;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemSocketingTests
    {
        [Fact]
        public void Item_AddToOpenSocket_SumsWeight()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();

            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(1)
                .WithWeight(100)
                .WithSocketTypeId(socketTypeId)
                .Build();

            var socketableItemData = new ProjectXyz.Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(
                    Stat.Create(statIdForSocketing, 1),
                    Stat.Create(ItemStats.Weight, 50))
                .Build();

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>();
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(StatSocketType.Create(statIdForSocketing, socketTypeId));

            var socketableItem = Item.Create(
                new MockItemContextBuilder().WithStatSocketTypeRepository(statSocketTypeRepository.Object).Build(),
                socketableItemData);

            // Execute
            var result = socketableItem.Socket(socketCandidate);
          
            // Assert
            Assert.True(
                result,
                "Expecting the socket operation to be successful.");
            Assert.Contains(socketCandidate, socketableItem.SocketedItems);
            Assert.Equal(
                socketableItem.GetTotalSocketsForType(socketCandidate.SocketTypeId) - socketCandidate.RequiredSockets,
                socketableItem.GetOpenSocketsForType(socketCandidate.SocketTypeId));
            Assert.Equal(150, socketableItem.Weight);
        }

        [Fact]
        public void Item_AddToOpenSocket_AppliesEnchantments()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();

            var socketCandidateEnchantment = new MockAdditiveEnchantmentBuilder()
                .WithStatId(ItemStats.Value)
                .WithValue(123456)
                .Build();

            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(1)
                .WithEnchantments(socketCandidateEnchantment)
                .WithSocketTypeId(socketTypeId)
                .Build();

            var socketableItemData = new ProjectXyz.Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(statIdForSocketing, 1))
                .Build();

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>();
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(StatSocketType.Create(statIdForSocketing, socketTypeId));

            var socketableItem = Item.Create(
                new MockItemContextBuilder().WithStatSocketTypeRepository(statSocketTypeRepository.Object).Build(),
                socketableItemData);

            // Execute
            var result = socketableItem.Socket(socketCandidate);

            // Assert
            Assert.True(
                result,
                "Expecting the socket operation to be successful.");
            Assert.Contains(socketCandidate, socketableItem.SocketedItems);
            Assert.Contains(socketCandidateEnchantment, socketableItem.Enchantments);
            Assert.Equal(socketCandidateEnchantment.Value, socketableItem.Value);
        }

        [Fact]
        public void Item_AddNoSocketsRequiredCandidateToOpenSocket_FailsToSocket()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();

            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(0)
                .Build();

            var socketableItemData = new ProjectXyz.Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(statIdForSocketing, 1))
                .Build();

            var socketableItem = Item.Create(
                new MockItemContextBuilder().Build(),
                socketableItemData);

            // Execute
            var result = socketableItem.Socket(socketCandidate);

            // Assert
            Assert.False(
                result,
                "Expecting the socket operation to be unsuccessful.");
            Assert.DoesNotContain(socketCandidate, socketableItem.SocketedItems);
        }

        [Fact]
        public void Item_TrySocketWithNoOpenSockets_FailsToSocket()
        {
            // Setup
            Guid statIdForClosedSockets = Guid.NewGuid();
            Guid closedSocketTypeId = Guid.NewGuid();
            Guid statIdForOpenSockets = Guid.NewGuid();
            Guid openSocketTypeId = Guid.NewGuid();

            var socketCandidate = new MockItemBuilder()
                .WithRequiredSockets(1)
                .WithSocketTypeId(closedSocketTypeId)
                .Build();

            var socketableItemData = new ProjectXyz.Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(statIdForClosedSockets, 0))
                .WithStats(Stat.Create(statIdForOpenSockets, 10))
                .Build();

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>();
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(closedSocketTypeId))
                .Returns(StatSocketType.Create(statIdForClosedSockets, closedSocketTypeId));
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(openSocketTypeId))
                .Returns(StatSocketType.Create(statIdForOpenSockets, openSocketTypeId));

            var socketableItem = Item.Create(
                new MockItemContextBuilder().WithStatSocketTypeRepository(statSocketTypeRepository.Object).Build(),
                socketableItemData);

            Assert.Equal(0, socketableItem.GetOpenSocketsForType(socketCandidate.SocketTypeId));

            // Execute
            var result = socketableItem.Socket(socketCandidate);

            // Assert
            Assert.False(
                result,
                "Expecting the second socket operation to fail.");
        }

        [Fact]
        public void Item_SocketedItemEnchantments_CanExpire()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();
            const double ENCHANTMENT_VALUE = 123456;
            var remainingDuration = TimeSpan.FromSeconds(5);

            var socketCandidateEnchantment = new Mock<IAdditiveEnchantment>(MockBehavior.Strict);
            socketCandidateEnchantment
                .Setup(x => x.StatId)
                .Returns(ItemStats.Value);
            socketCandidateEnchantment
                .Setup(x => x.Value)
                .Returns(ENCHANTMENT_VALUE);
            socketCandidateEnchantment
                .Setup(x => x.WeatherIds)
                .Returns(Enumerable.Empty<Guid>());
            socketCandidateEnchantment
                .Setup(x => x.UpdateElapsedTime(remainingDuration))
                .Callback(() =>
                {
                    socketCandidateEnchantment.Raise(x => x.Expired += null, EventArgs.Empty);
                });

            var socketCandidate = new MockItemBuilder()
                .WithEnchantments(socketCandidateEnchantment.Object)
                .WithRequiredSockets(1)
                .WithSocketTypeId(socketTypeId)
                .Build();

            var socketableItemData = new ProjectXyz.Data.Tests.Items.Mocks.MockItemBuilder()
                .WithStats(Stat.Create(statIdForSocketing, 1))
                .Build();
            
            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>();
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(StatSocketType.Create(statIdForSocketing, socketTypeId));

            var socketableItem = Item.Create(
                new MockItemContextBuilder().WithStatSocketTypeRepository(statSocketTypeRepository.Object).Build(),
                socketableItemData);
            Assert.True(
                socketableItem.Socket(socketCandidate),
                "Expecting the socket operation to be successful.");
            Assert.Equal(ENCHANTMENT_VALUE, socketableItem.Value);

            // Execute
            socketableItem.UpdateElapsedTime(remainingDuration);

            // Assert
            Assert.DoesNotContain(socketCandidateEnchantment.Object, socketableItem.Enchantments);
            Assert.Equal(0, socketableItem.Value);

            socketCandidateEnchantment.Verify(x => x.StatId, Times.Exactly(6));
            socketCandidateEnchantment.Verify(x => x.Value, Times.Exactly(2));
            socketCandidateEnchantment.Verify(x => x.WeatherIds, Times.Exactly(2));
            socketCandidateEnchantment.Verify(x => x.UpdateElapsedTime(It.IsAny<TimeSpan>()), Times.Once());
        }
    }
}
