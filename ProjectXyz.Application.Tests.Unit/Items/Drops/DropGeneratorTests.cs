using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Application.Core.Items.Drops;
using ProjectXyz.Application.Interface;
using ProjectXyz.Application.Interface.Items;
using ProjectXyz.Data.Interface.Items.Drops;
using ProjectXyz.Data.Interface.Items.MagicTypes;
using Xunit;

namespace ProjectXyz.Application.Tests.Unit.Items.Drops
{
    public class DropGeneratorTests
    {
        #region Methods
        [Fact]
        private void Generate_SingleDropEntry_SingleItem()
        {
            // Setup
            var dropId = Guid.NewGuid();
            var itemDefinitionId = Guid.NewGuid();
            var magicTypeId = Guid.NewGuid();
            var magicTypeGroupingId = Guid.NewGuid();
            const int LEVEL = 0;
            const int MINIMUM_DROP_COUNT = 1;
            const int MAXIMUM_DROP_COUNT = 1;
            const int DROP_WEIGHTING = 1;

            var randomizer = new Mock<IRandom>(MockBehavior.Strict);
            randomizer
                .SetupSequence(x => x.NextDouble())
                .Returns(0)
                .Returns(0)
                .Returns(0);

            var itemContext = new Mock<IItemContext>(MockBehavior.Strict);

            var drop = new Mock<IDrop>(MockBehavior.Strict);
            drop
                .Setup(x => x.Minimum)
                .Returns(MINIMUM_DROP_COUNT);
            drop
                .Setup(x => x.Maximum)
                .Returns(MAXIMUM_DROP_COUNT);
            drop
                .Setup(x => x.Id)
                .Returns(dropId);
            drop
                .Setup(x => x.CanRepeat)
                .Returns(false);

            var dropRepository = new Mock<IDropRepository>(MockBehavior.Strict);
            dropRepository
                .Setup(x => x.GetById(dropId))
                .Returns(drop.Object);

            var dropEntry = new Mock<IDropEntry>(MockBehavior.Strict);
            dropEntry
                .Setup(x => x.Weighting)
                .Returns(DROP_WEIGHTING);
            dropEntry
                .Setup(x => x.ItemDefinitionId)
                .Returns(itemDefinitionId);
            dropEntry
                .Setup(x => x.MagicTypeGroupingId)
                .Returns(magicTypeGroupingId);

            var dropEntryRepository = new Mock<IDropEntryRepository>(MockBehavior.Strict);
            dropEntryRepository
                .Setup(x => x.GetByParentDropId(dropId))
                .Returns(new[] { dropEntry.Object });

            var dropLinkRepository = new Mock<IDropLinkRepository>(MockBehavior.Strict);
            dropLinkRepository
                .Setup(x => x.GetByParentDropId(dropId))
                .Returns(new IDropLink[0]);

            var magicTypeGrouping = new Mock<IMagicTypeGrouping>(MockBehavior.Strict);
            magicTypeGrouping
                .Setup(x => x.MagicTypeId)
                .Returns(magicTypeId);

            var magicTypeGroupingRepository = new Mock<IMagicTypeGroupingRepository>(MockBehavior.Strict);
            magicTypeGroupingRepository
                .Setup(x => x.GetByGroupingId(magicTypeGroupingId))
                .Returns(new[] { magicTypeGrouping.Object });

            var magicType = new Mock<IMagicType>(MockBehavior.Strict);
            magicType
                .Setup(x => x.Id)
                .Returns(magicTypeId);
            magicType
                .Setup(x => x.RarityWeighting)
                .Returns(1);

            var magicTypeRepository = new Mock<IMagicTypeRepository>(MockBehavior.Strict);
            magicTypeRepository
                .Setup(x => x.GetAll())
                .Returns(new[] { magicType.Object });

            var item = new Mock<IItem>(MockBehavior.Strict);

            var itemGenerator = new Mock<IItemGenerator>(MockBehavior.Strict);
            itemGenerator
                .Setup(x => x.Generate(
                    randomizer.Object,
                    itemDefinitionId,
                    It.IsAny<Guid>(),
                    LEVEL,
                    itemContext.Object))
                .Returns(item.Object);

            var itemDropGenerator = DropGenerator.Create(
                dropRepository.Object,
                dropEntryRepository.Object,
                dropLinkRepository.Object,
                magicTypeRepository.Object,
                magicTypeGroupingRepository.Object,
                itemGenerator.Object);

            // Execute
            var result = itemDropGenerator.Generate(
                randomizer.Object,
                dropId,
                LEVEL,
                itemContext.Object)
                .ToArray();

            // Assert
            Assert.Equal(1, result.Length);
            Assert.Equal(item.Object, result.First());

            randomizer.Verify(x => x.NextDouble(), Times.Exactly(3));

            drop.Verify(x => x.Minimum, Times.Exactly(2));
            drop.Verify(x => x.Maximum, Times.Once);
            drop.Verify(x => x.Id, Times.Once);
            drop.Verify(x => x.CanRepeat, Times.Exactly(2));

            dropRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);

            dropEntry.Verify(x => x.Weighting, Times.Exactly(3));
            dropEntry.Verify(x => x.ItemDefinitionId, Times.Once);
            dropEntry.Verify(x => x.MagicTypeGroupingId, Times.Once);

            dropEntryRepository.Verify(x => x.GetByParentDropId(It.IsAny<Guid>()), Times.Once);

            dropLinkRepository.Verify(x => x.GetByParentDropId(It.IsAny<Guid>()), Times.Once);

            magicTypeGrouping.Verify(x => x.MagicTypeId, Times.Once);

            magicTypeGroupingRepository.Verify(x => x.GetByGroupingId(It.IsAny<Guid>()), Times.Once);

            itemGenerator.Verify(
                x => x.Generate(
                    It.IsAny<IRandom>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<IItemContext>()),
                Times.Once);

            magicType.Verify(x => x.Id, Times.Exactly(2));
            magicType.Verify(x => x.RarityWeighting, Times.Exactly(2));

            magicTypeRepository.Verify(x => x.GetAll(), Times.Once);
        }
        #endregion
    }
}
