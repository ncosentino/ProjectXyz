using System.Collections.Generic;
using Moq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;
using Xunit;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Tests
{
    public sealed class CanEquipBehaviorTests
    {
        private readonly MockRepository _mockRepository;
        private readonly CanEquipBehavior _canEquipBehavior;
        private readonly Mock<ICanBeEquippedBehavior> _mockCanBeEquipped;
        private readonly IReadOnlyCollection<IIdentifier> _supportedEquipSlotIds;

        public CanEquipBehaviorTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockCanBeEquipped = _mockRepository.Create<ICanBeEquippedBehavior>();
            _supportedEquipSlotIds = new[]
            {
                new StringIdentifier("body")
            };
            _canEquipBehavior = new CanEquipBehavior(_supportedEquipSlotIds);
        }

        [Fact]
        private void TryEquip_CanBeEquipped_InvokeEventReturnTrue()
        {
            var equipSlotId = new StringIdentifier("body");

            _mockCanBeEquipped
                .Setup(x => x.AllowedEquipSlots)
                .Returns(_supportedEquipSlotIds);

            var equippedCount = 0;
            _canEquipBehavior.Equipped += (_, __) => equippedCount++;

            var result = _canEquipBehavior.TryEquip(
                equipSlotId,
                _mockCanBeEquipped.Object);

            Assert.True(
                result,
                "Unexpected result for TryEquip().");
            Assert.Equal(1, equippedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void TryEquip_NotSupported_ReturnFalse()
        {
            var equipSlotId = new StringIdentifier("not supported slot");

            var equippedCount = 0;
            _canEquipBehavior.Equipped += (_, __) => equippedCount++;

            var result = _canEquipBehavior.TryEquip(
                equipSlotId,
                _mockCanBeEquipped.Object);

            Assert.False(
                result,
                "Unexpected result for TryEquip().");
            Assert.Equal(0, equippedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void TryEquip_AlreadyEquipped_FailsToEquip()
        {
            var equipSlotId = new StringIdentifier("body");

            _mockCanBeEquipped
                .Setup(x => x.AllowedEquipSlots)
                .Returns(_supportedEquipSlotIds);

            _canEquipBehavior.TryEquip(
                equipSlotId,
                _mockCanBeEquipped.Object);

            var equippedCount = 0;
            _canEquipBehavior.Equipped += (_, __) => equippedCount++;

            var result = _canEquipBehavior.TryEquip(
                equipSlotId,
                _mockCanBeEquipped.Object);

            Assert.False(
                result,
                "Unexpected result for TryEquip().");
            Assert.Equal(0, equippedCount);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void TryUnequip_NothingEquipped_FailsToUnequip()
        {
            var equipSlotId = new StringIdentifier("body");

            var unequippedCount = 0;
            _canEquipBehavior.Unequipped += (_, __) => unequippedCount++;

            ICanBeEquippedBehavior output;
            var result = _canEquipBehavior.TryUnequip(
                equipSlotId,
                out output);

            Assert.False(
                result,
                "Unexpected result for TryUnequip().");
            Assert.Equal(0, unequippedCount);
            Assert.Null(output);
            _mockRepository.VerifyAll();
        }

        [Fact]
        private void TryUnequip_HasEquipment_InvokesEventReturnsTrue()
        {
            var equipSlotId = new StringIdentifier("body");

            _mockCanBeEquipped
                .Setup(x => x.AllowedEquipSlots)
                .Returns(_supportedEquipSlotIds);

            _canEquipBehavior.TryEquip(
                equipSlotId,
                _mockCanBeEquipped.Object);

            var unequippedCount = 0;
            _canEquipBehavior.Unequipped += (_, __) => unequippedCount++;

            ICanBeEquippedBehavior output;
            var result = _canEquipBehavior.TryUnequip(
                equipSlotId,
                out output);

            Assert.True(
                result,
                "Unexpected result for TryUnequip().");
            Assert.Equal(1, unequippedCount);
            Assert.Equal(_mockCanBeEquipped.Object, output);
            _mockRepository.VerifyAll();
        }
    }
}
