using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Tests.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentDefinitionRepositoryTests
    {
        #region Constants
        private const string COLUMN_NAME_ID = "Id";
        private const int COLUMN_INDEX_ID = 0;

        private const string COLUMN_NAME_STAT_ID = "StatId";
        private const int COLUMN_INDEX_STAT_ID = COLUMN_INDEX_ID + 1;

        private const string COLUMN_NAME_CALCULATION_ID = "CalculationId";
        private const int COLUMN_INDEX_CALCULATION_ID = COLUMN_INDEX_STAT_ID + 1;

        private const string COLUMN_NAME_TRIGGER_ID = "TriggerId";
        private const int COLUMN_INDEX_TRIGGER_ID = COLUMN_INDEX_CALCULATION_ID + 1;

        private const string COLUMN_NAME_STATUS_TYPE_ID = "StatusTypeId";
        private const int COLUMN_INDEX_STATUS_TYPE_ID = COLUMN_INDEX_TRIGGER_ID + 1;

        private const string COLUMN_NAME_MINIMUM_VALUE_ID = "MinimumValue";
        private const int COLUMN_INDEX_MINIMUM_VALUE_ID = COLUMN_INDEX_STATUS_TYPE_ID + 1;

        private const string COLUMN_NAME_MAXIMUM_VALUE_ID = "MaximumValue";
        private const int COLUMN_INDEX_MAXIMUM_VALUE_ID = COLUMN_INDEX_MINIMUM_VALUE_ID + 1;

        private const string COLUMN_NAME_MINIMUM_DURATION_ID = "MinimumDuration";
        private const int COLUMN_INDEX_MINIMUM_DURATION_ID = COLUMN_INDEX_MAXIMUM_VALUE_ID + 1;

        private const string COLUMN_NAME_MAXIMUM_DURATION_ID = "MaximumDuration";
        private const int COLUMN_INDEX_MAXIMUM_DURATION_ID = COLUMN_INDEX_MINIMUM_DURATION_ID + 1;
        #endregion

        #region Methods
        [Fact]
        public void EnchantmentDefinitionRepository_GetById_ExpectedValues()
        {
            Guid DEFINITION_ID = new Guid();
            Guid STAT_ID = new Guid();
            Guid CALCULATION_ID = new Guid();
            Guid TRIGGER_ID = new Guid();
            Guid STATUS_TYPE_ID = new Guid();
            
            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(true);
            
            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_ID))
                .Returns(COLUMN_INDEX_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_ID))
                .Returns(DEFINITION_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STAT_ID))
                .Returns(COLUMN_INDEX_STAT_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_STAT_ID))
                .Returns(STAT_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_CALCULATION_ID))
                .Returns(COLUMN_INDEX_CALCULATION_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_CALCULATION_ID))
                .Returns(CALCULATION_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_TRIGGER_ID))
                .Returns(COLUMN_INDEX_TRIGGER_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_TRIGGER_ID))
                .Returns(TRIGGER_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STATUS_TYPE_ID))
                .Returns(COLUMN_INDEX_STATUS_TYPE_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_STATUS_TYPE_ID))
                .Returns(STATUS_TYPE_ID);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MINIMUM_VALUE_ID))
                .Returns(COLUMN_INDEX_MINIMUM_VALUE_ID);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MINIMUM_VALUE_ID))
                .Returns(0);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MAXIMUM_VALUE_ID))
                .Returns(COLUMN_INDEX_MAXIMUM_VALUE_ID);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MAXIMUM_VALUE_ID))
                .Returns(100);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MINIMUM_DURATION_ID))
                .Returns(COLUMN_INDEX_MINIMUM_DURATION_ID);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MINIMUM_DURATION_ID))
                .Returns(1000);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MAXIMUM_DURATION_ID))
                .Returns(COLUMN_INDEX_MAXIMUM_DURATION_ID);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MAXIMUM_DURATION_ID))
                .Returns(5000);
            
            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantmentDefinition = new Mock<IEnchantmentDefinition>();

            var factory = new Mock<IEnchantmentDefinitionFactory>();
            factory
                .Setup(x => x.CreateEnchantmentDefinition(
                    DEFINITION_ID, 
                    STAT_ID,
                    CALCULATION_ID,
                    TRIGGER_ID,
                    STATUS_TYPE_ID,
                    0,
                    100,
                    TimeSpan.FromMilliseconds(1000),
                    TimeSpan.FromMilliseconds(5000)))
                .Returns(enchantmentDefinition.Object);

            var repository = EnchantmentDefinitionRepository.Create(
                database.Object, 
                factory.Object);
           
            var result = repository.GetById(DEFINITION_ID);

            Assert.Equal(enchantmentDefinition.Object, result);

            factory.Verify(x => x.CreateEnchantmentDefinition(
                    DEFINITION_ID,
                    STAT_ID,
                    CALCULATION_ID,
                    TRIGGER_ID,
                    STATUS_TYPE_ID,
                    0,
                    100,
                    TimeSpan.FromMilliseconds(1000),
                    TimeSpan.FromMilliseconds(5000)),
                    Times.Once);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
        }

        [Fact]
        public void EnchantmentDefinitionRepository_GetByIdNotAvailable_Throws()
        {
            Guid DEFINITION_ID = new Guid();

            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(false);

            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantmentDefinition = new Mock<IEnchantmentDefinition>();

            var factory = new Mock<IEnchantmentDefinitionFactory>();

            var repository = EnchantmentDefinitionRepository.Create(
                database.Object,
                factory.Object);

            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetById(DEFINITION_ID));
            Assert.Equal("No enchantment with Id '" + DEFINITION_ID + "' was found.", exception.Message);

            factory.Verify(x => x.CreateEnchantmentDefinition(
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>(),
                    It.IsAny<double>(),
                    It.IsAny<double>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<TimeSpan>()),
                    Times.Never);

            database.Verify(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);

            command.Verify(x => x.ExecuteReader(), Times.Once);
        }
        #endregion
    }
}