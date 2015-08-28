using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ProjectXyz.Data.Interface.Items.EquipSlots;
using ProjectXyz.Data.Sql.Items.EquipSlots;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Integration.Items.EquipSlots
{
    [DataLayer]
    [Enchantments]
    public class EquipSlotTypeRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void GetAll_NoItems_NoResults()
        {
            // Setup
            var factory = new Mock<IEquipSlotTypeFactory>(MockBehavior.Strict);

            var repository = EquipSlotTypeRepository.Create(
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
            var genericCollectionResult = new Mock<IEquipSlotType>(MockBehavior.Strict);

            var factory = new Mock<IEquipSlotTypeFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()))
                .Returns(genericCollectionResult.Object);

            var repository = EquipSlotTypeRepository.Create(
                Database,
                factory.Object);

            const int NUMBER_OF_RESULTS = 5;
            for (int i = 0; i < NUMBER_OF_RESULTS; i++)
            {
                CreateEquipSlotType(
                    Guid.NewGuid(),
                    Guid.NewGuid());
            }

            // Execute
            var result = repository.GetAll();

            // Assert
            Assert.Equal(NUMBER_OF_RESULTS, result.Count());

            factory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Exactly(NUMBER_OF_RESULTS));
        }

        [Fact]
        public void GetById_NoItem_ThrowsException()
        {
            // Setup
            var factory = new Mock<IEquipSlotTypeFactory>(MockBehavior.Strict);

            var repository = EquipSlotTypeRepository.Create(
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
            var expectedResult = new Mock<IEquipSlotType>(MockBehavior.Strict);

            var id = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();

            var factory = new Mock<IEquipSlotTypeFactory>(MockBehavior.Strict);
            factory
                .Setup(x => x.Create(
                    id,
                    nameStringResourceId))
                .Returns(expectedResult.Object);

            var repository = EquipSlotTypeRepository.Create(
                Database,
                factory.Object);

            CreateEquipSlotType(
                id,
                nameStringResourceId);

            // Execute
            var result = repository.GetById(id);

            // Assert
            Assert.Equal(expectedResult.Object, result);

            factory.Verify(
                x => x.Create(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()),
                Times.Once);
        }

        [Fact]
        public void RemoveById_ItemExists_RemovedFromDatabase()
        {
            // Setup
            var factory = new Mock<IEquipSlotTypeFactory>(MockBehavior.Strict);
            
            var repository = EquipSlotTypeRepository.Create(
                Database,
                factory.Object);

            var id = Guid.NewGuid();
            CreateEquipSlotType(
                id,
                Guid.NewGuid());

            // Execute
            repository.RemoveById(id);

            // Assert
            using (var command = Database.CreateCommand("SELECT * FROM EquipSlotTypes"))
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

            var entryToAdd = new Mock<IEquipSlotType>(MockBehavior.Strict);
            entryToAdd
                .Setup(x => x.Id)
                .Returns(id);
            entryToAdd
                .Setup(x => x.NameStringResourceId)
                .Returns(nameStringResourceId);

            var factory = new Mock<IEquipSlotTypeFactory>(MockBehavior.Strict);

            var repository = EquipSlotTypeRepository.Create(
                Database,
                factory.Object);


            // Execute
            repository.Add(entryToAdd.Object);

            // Assert
            using (var command = Database.CreateCommand("SELECT * FROM EquipSlotTypes"))
            {
                using (var reader = command.ExecuteReader())
                {
                    Assert.True(reader.Read(), "Expecting to read one row.");
                    Assert.False(reader.Read(), "Not expecting additional rows.");
                }
            }

            entryToAdd.Verify(x => x.Id, Times.Once);
            entryToAdd.Verify(x => x.NameStringResourceId, Times.Once);
        }

        private void CreateEquipSlotType(
            Guid id,
            Guid nameStringResourceId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "NameStringResourceId", nameStringResourceId },
            };

            using (var command = Database.CreateCommand(
                @"
                INSERT INTO
                    EquipSlotTypes
                (
                    Id,
                    NameStringResourceId
                )
                VALUES
                (
                    @Id,
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