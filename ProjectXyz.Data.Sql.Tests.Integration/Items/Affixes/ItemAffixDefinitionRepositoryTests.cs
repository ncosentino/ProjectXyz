using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Data.Core.Items.Affixes;
using ProjectXyz.Data.Interface.Items.Affixes;
using ProjectXyz.Data.Sql.Items.Affixes;
using ProjectXyz.Tests.Integration;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Integration.Items.Affixes
{
    [DataLayer]
    [Enchantments]
    public class ItemAffixDefinitionRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void GetAll_NoItems_NoResults()
        {
            // Setup
            var factory = new Mock<IItemAffixDefinitionFactory>(MockBehavior.Strict);

            var repository = ItemAffixDefinitionRepository.Create(
                Database,
                factory.Object);

            // Execute
            var result = repository.GetAll();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAll_MultipleItems_MultipleResults()
        {
            // Setup
            var genericCollectionResult = new Mock<IItemAffixDefinition>(MockBehavior.Strict);

            var factory = new Mock<IItemAffixDefinitionFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .Returns(genericCollectionResult.Object);

            var repository = ItemAffixDefinitionRepository.Create(
                Database,
                factory.Object);

            const int NUMBER_OF_RESULTS = 5;
            for (int i = 0; i < NUMBER_OF_RESULTS; i++)
            {
                CreateDefinition(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    true,
                    123,
                    456);
            }

            // Execute
            var result = repository.GetAll();

            // Assert
            Assert.Equal(NUMBER_OF_RESULTS, result.Count());

            factory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Exactly(NUMBER_OF_RESULTS));
        }

        [Fact]
        public void GetById_NoItem_ThrowsException()
        {
            // Setup
            var factory = new Mock<IItemAffixDefinitionFactory>(MockBehavior.Strict);

            var repository = ItemAffixDefinitionRepository.Create(
                Database,
                factory.Object);

            // Execute
            Assert.ThrowsDelegateWithReturn method = () => repository.GetById(Guid.NewGuid());

            // Assert
            var result = Assert.Throws<InvalidOperationException>(method);
        }

        [Fact]
        public void GetById_ItemExists_ExpectedResult()
        {
            // Setup
            var expectedResult = new Mock<IItemAffixDefinition>(MockBehavior.Strict);

            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            const bool IS_PREFIX = true;
            const int MINIMUM_LEVEL = 123;
            const int MAXIMUMLEVEL = 456;

            var factory = new Mock<IItemAffixDefinitionFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(
                    id,
                    nameStringResourceId,
                    IS_PREFIX,
                    MINIMUM_LEVEL,
                    MAXIMUMLEVEL))
                .Returns(expectedResult.Object);

            var repository = ItemAffixDefinitionRepository.Create(
                Database,
                factory.Object);

            CreateDefinition(
                id,
                nameStringResourceId,
                IS_PREFIX,
                MINIMUM_LEVEL,
                MAXIMUMLEVEL);

            // Execute
            var result = repository.GetById(id);

            // Assert
            Assert.Equal(expectedResult.Object, result);

            factory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Once);
        }

        [Fact]
        public void RemoveById_ItemExists_RemovedFromDatabase()
        {
            // Setup
            var factory = new Mock<IItemAffixDefinitionFactory>(MockBehavior.Strict);
            
            var repository = ItemAffixDefinitionRepository.Create(
                Database,
                factory.Object);

            var id = Guid.NewGuid();
            CreateDefinition(
                id,
                Guid.NewGuid(),
                true,
                123,
                456);

            // Execute
            repository.RemoveById(id);

            // Assert
            using (var command = Database.CreateCommand("SELECT * FROM ItemAffixDefinitions"))
            {
                using (var reader = command.ExecuteReader())
                {
                    Assert.False(reader.Read(), "Not expecting any rows.");
                }
            }
        }

        [Fact]
        public void Add_ValidItem_AddedToDatabase()
        {
            // Setup
            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();
            const bool IS_PREFIX = true;
            const int MINIMUM_LEVEL = 123;
            const int MAXIMUM_LEVEL = 456;
            var factory = ItemAffixDefinitionFactory.Create();

            var repository = ItemAffixDefinitionRepository.Create(
                Database,
                factory);

            // Execute
            repository.Add(
                id,
                nameStringResourceId,
                IS_PREFIX,
                MINIMUM_LEVEL,
                MAXIMUM_LEVEL);

            // Assert
            using (var command = Database.CreateCommand("SELECT * FROM ItemAffixDefinitions"))
            {
                using (var reader = command.ExecuteReader())
                {
                    Assert.True(reader.Read(), "Expecting to read one row.");
                    Assert.False(reader.Read(), "Not expecting additional rows.");
                }
            }
        }

        private void CreateDefinition(
            Guid id,
            Guid nameStringResourceId,
            bool isPrefix,
            int minimumLevel,
            int maximumLevel)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "IsPrefix", isPrefix },
                { "MaximumLevel", maximumLevel},
                { "MinimumLevel", minimumLevel },
                { "NameStringResourceId", nameStringResourceId },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    ItemAffixDefinitions
                (
                    Id,
                    IsPrefix,
                    MaximumLevel,
                    MinimumLevel,
                    NameStringResourceId
                )
                VALUES
                (
                    @Id,
                    @IsPrefix,
                    @MaximumLevel,
                    @MinimumLevel,
                    @NameStringResourceId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }
        }
        #endregion
    }
}