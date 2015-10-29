using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items;
using ProjectXyz.Application.Interface.Enchantments;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Application.Interface.Items.Affixes;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Items.Requirements;
using ProjectXyz.Application.Tests.Integration.Helpers;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Core.Items.Sockets;
using ProjectXyz.Data.Core.Stats;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;
using ProjectXyz.Data.Interface.Items.Sockets;
using ProjectXyz.Data.Interface.Stats;
using ProjectXyz.Plugins.Enchantments.Expression;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Application.Tests.Integration.Items
{
    [ApplicationLayer]
    [Items]
    public class ItemTests
    {
        #region Methods
        [Fact]
        public void Item_AddToOpenSocket_SumsWeight()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);
            itemMetaData
                .Setup(x => x.SocketTypeId)
                .Returns(socketTypeId);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var statSocketType = StatSocketType.Create(Guid.NewGuid(), statIdForSocketing, socketTypeId);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(statSocketType);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statSocketType });

            var itemContext = ItemContextHelper.CreateItemContext(statSocketTypeRepository: statSocketTypeRepository.Object);

            var socketCandidate = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.Weight, 100),
                    Stat.Create(Guid.NewGuid(), ItemStats.RequiredSockets, 1),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            var socketableItem = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), statIdForSocketing, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.Weight, 50),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

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
            const double ENCHANTMENT_VALUE = 123456;

            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);
            itemMetaData
                .Setup(x => x.SocketTypeId)
                .Returns(socketTypeId);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var statSocketType = StatSocketType.Create(Guid.NewGuid(), statIdForSocketing, socketTypeId);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(statSocketType);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statSocketType });

            var itemContext = ItemContextHelper.CreateItemContext(statSocketTypeRepository: statSocketTypeRepository.Object);

            var socketCandidateEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Item,
                Guid.NewGuid(),
                Guid.NewGuid(),
                TimeSpan.Zero,
                ItemStats.Value,
                "VALUE",
                0,
                Enumerable.Empty<KeyValuePair<string, Guid>>(),
                new[] { new KeyValuePair<string, double>("VALUE", ENCHANTMENT_VALUE) });

            var socketCandidate = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.Weight, 100),
                    Stat.Create(Guid.NewGuid(), ItemStats.RequiredSockets, 1),
                },
                new[]
                {
                    socketCandidateEnchantment,
                },
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            var socketableItem = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), statIdForSocketing, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.Weight, 50),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());


            // Execute
            var result = socketableItem.Socket(socketCandidate);

            // Assert
            Assert.True(
                result,
                "Expecting the socket operation to be successful.");
            Assert.Contains(socketCandidate, socketableItem.SocketedItems);
            Assert.Contains(socketCandidateEnchantment, socketableItem.Enchantments);
            Assert.Equal(ENCHANTMENT_VALUE, socketableItem.Value);
        }

        [Fact]
        public void Item_AddNoSocketsRequiredCandidateToOpenSocket_FailsToSocket()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);
            itemMetaData
                .Setup(x => x.SocketTypeId)
                .Returns(socketTypeId);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var statSocketType = StatSocketType.Create(Guid.NewGuid(), statIdForSocketing, socketTypeId);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(statSocketType);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statSocketType });

            var itemContext = ItemContextHelper.CreateItemContext(statSocketTypeRepository: statSocketTypeRepository.Object);

            var socketCandidate = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                Enumerable.Empty<IStat>(),
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            var socketableItem = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), statIdForSocketing, 1),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

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

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);
            itemMetaData
                .Setup(x => x.SocketTypeId)
                .Returns(closedSocketTypeId);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var closedStatSocketType = StatSocketType.Create(Guid.NewGuid(), statIdForClosedSockets, closedSocketTypeId);
            var openStatSocketType = StatSocketType.Create(Guid.NewGuid(), statIdForOpenSockets, openSocketTypeId);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(closedSocketTypeId))
                .Returns(closedStatSocketType);
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(openSocketTypeId))
                .Returns(openStatSocketType);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { closedStatSocketType, openStatSocketType });

            var itemContext = ItemContextHelper.CreateItemContext(statSocketTypeRepository: statSocketTypeRepository.Object);

            var socketCandidate = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.RequiredSockets, 1),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            var socketableItem = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), openSocketTypeId, 1),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            // Execute
            var result = socketableItem.Socket(socketCandidate);

            // Assert
            Assert.False(
                result,
                "Expecting the socket operation to be unsuccessful.");
            Assert.DoesNotContain(socketCandidate, socketableItem.SocketedItems);
        }

        [Fact]
        public void Item_SocketedItemEnchantments_CanExpire()
        {
            // Setup
            Guid statIdForSocketing = Guid.NewGuid();
            Guid socketTypeId = Guid.NewGuid();
            const double ENCHANTMENT_VALUE = 123456;
            const double BASE_VALUE = 100;
            var remainingDuration = TimeSpan.FromSeconds(5);

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);
            itemMetaData
                .Setup(x => x.SocketTypeId)
                .Returns(socketTypeId);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var statSocketType = StatSocketType.Create(Guid.NewGuid(), statIdForSocketing, socketTypeId);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetBySocketTypeId(socketTypeId))
                .Returns(statSocketType);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { statSocketType });

            var itemContext = ItemContextHelper.CreateItemContext(statSocketTypeRepository: statSocketTypeRepository.Object);

            var socketCandidateEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Item,
                Guid.NewGuid(),
                Guid.NewGuid(),
                remainingDuration,
                ItemStats.Value,
                "BASE + BONUS",
                0,
                new[] { new KeyValuePair<string, Guid>("BASE", ItemStats.Value) },
                new[] { new KeyValuePair<string, double>("BONUS", ENCHANTMENT_VALUE) });

            var socketCandidate = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.RequiredSockets, 1),
                },
                new[]
                {
                    socketCandidateEnchantment,
                },
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            var socketableItem = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), statIdForSocketing, 1),
                    Stat.Create(Guid.NewGuid(), ItemStats.Value, BASE_VALUE),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            var socketResult = socketableItem.Socket(socketCandidate);
            Assert.True(
                socketResult,
                "Expecting the socket operation to be successful.");
            Assert.Equal(BASE_VALUE + ENCHANTMENT_VALUE, socketableItem.Value);

            // Execute
            socketableItem.UpdateElapsedTime(remainingDuration);

            // Assert
            Assert.DoesNotContain(socketCandidateEnchantment, socketableItem.Enchantments);
            Assert.Equal(BASE_VALUE, socketableItem.Value);
        }

        [Fact]
        public void Item_EnchantNegativeCurrentDurability_BreaksItem()
        {
            // Setup
            const double BASE_MAX_DURABILITY = 50;
            const double ENCHANTMENT_VALUE = -2 * BASE_MAX_DURABILITY;

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);
            
            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<IStatSocketType>());

            var itemContext = ItemContextHelper.CreateItemContext(statSocketTypeRepository: statSocketTypeRepository.Object);

            var breakingEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                EnchantmentTriggers.Item,
                Guid.NewGuid(),
                Guid.NewGuid(),
                TimeSpan.Zero,
                ItemStats.CurrentDurability,
                "VALUE",
                0,
                Enumerable.Empty<KeyValuePair<string, Guid>>(),
                new[] { new KeyValuePair<string, double>("VALUE", ENCHANTMENT_VALUE) });
            
            var item = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, BASE_MAX_DURABILITY),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, BASE_MAX_DURABILITY),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            bool gotEvent = false;
            item.DurabilityChanged += (sender, __) =>
            {
                if (((IItem)sender).CurrentDurability <= 0)
                {
                    gotEvent = true;
                }
            };

            // Execute
            item.Enchant(breakingEnchantment);
            
            // Asseert
            Assert.True(
                gotEvent,
                "Expecting to get broken event.");
            Assert.Equal(0, item.CurrentDurability);
            Assert.Equal(BASE_MAX_DURABILITY, item.MaximumDurability);
        }

        [Fact]
        public void Item_EnchantNegativeDurabilityBrokenItem_DoesNotBreak()
        {
            // Setup
            const double BASE_MAX_DURABILITY = -1;
            const double ENCHANTMENT_VALUE = -50;

            var itemMetaData = new Mock<IItemMetaData>(MockBehavior.Strict);

            var itemRequirements = new Mock<IItemRequirements>(MockBehavior.Strict);

            var statSocketTypeRepository = new Mock<IStatSocketTypeRepository>(MockBehavior.Strict);
            statSocketTypeRepository
                .Setup(x => x.GetAll())
                .Returns(Enumerable.Empty<IStatSocketType>());

            var itemContext = ItemContextHelper.CreateItemContext(statSocketTypeRepository: statSocketTypeRepository.Object);

            var breakingEnchantment = ExpressionEnchantment.Create(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                TimeSpan.Zero,
                ItemStats.CurrentDurability,
                "VALUE",
                0,
                Enumerable.Empty<KeyValuePair<string, Guid>>(),
                new[] { new KeyValuePair<string, double>("VALUE", ENCHANTMENT_VALUE) });

            var item = Item.Create(
                itemContext,
                Guid.NewGuid(),
                Guid.NewGuid(),
                itemMetaData.Object,
                Enumerable.Empty<IItemNamePart>(),
                itemRequirements.Object,
                new[]
                {
                    Stat.Create(Guid.NewGuid(), ItemStats.CurrentDurability, BASE_MAX_DURABILITY),
                    Stat.Create(Guid.NewGuid(), ItemStats.MaximumDurability, BASE_MAX_DURABILITY),
                },
                Enumerable.Empty<IEnchantment>(),
                Enumerable.Empty<IItemAffix>(),
                Enumerable.Empty<IItem>(),
                Enumerable.Empty<Guid>());

            bool gotEvent = false;
            item.DurabilityChanged += (sender, __) =>
            {
                if (((IItem)sender).CurrentDurability <= 0)
                {
                    gotEvent = true;
                }
            };

            // Execute
            item.Enchant(breakingEnchantment);

            // Asseert
            Assert.False(
                gotEvent,
                "Not expecting to get broken event.");
        }
        #endregion
    }
}