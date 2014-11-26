using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Moq;

using ProjectXyz.Tests.Xunit.Categories;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Data.Sql.Tests.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentRepositoryTests
    {
        #region Constants
        private const string COLUMN_NAME_STAT_ID = "StatId";
        private const int COLUMN_INDEX_STAT_ID = 0;

        private const string COLUMN_NAME_CALCULATION_ID = "CalculationId";
        private const int COLUMN_INDEX_CALCULATION_ID = COLUMN_INDEX_STAT_ID + 1;

        private const string COLUMN_NAME_TRIGGER_ID = "TriggerId";
        private const int COLUMN_INDEX_TRIGGER_ID = COLUMN_INDEX_CALCULATION_ID + 1;

        private const string COLUMN_NAME_STATUS_TYPE_ID = "StatusTypeId";
        private const int COLUMN_INDEX_STATUS_TYPE_ID = COLUMN_INDEX_TRIGGER_ID + 1;

        private const string COLUMN_NAME_MAXIMUM_DURATION = "MaximumDuration";
        private const int COLUMN_INDEX_MAXIMUM_DURATION = COLUMN_INDEX_STATUS_TYPE_ID + 1;

        private const string COLUMN_NAME_MINIMUM_DURATION = "MinimumDuration";
        private const int COLUMN_INDEX_MINIMUM_DURATION = COLUMN_INDEX_MAXIMUM_DURATION + 1;

        private const string COLUMN_NAME_MAXIMUM_VALUE = "MaximumValue";
        private const int COLUMN_INDEX_MAXIMUM_VALUE = COLUMN_INDEX_MINIMUM_DURATION + 1;

        private const string COLUMN_NAME_MINIMUM_VALUE = "MinimumValue";
        private const int COLUMN_INDEX_MINIMUM_VALUE = COLUMN_INDEX_MAXIMUM_VALUE + 1;
        #endregion

        #region Methods
        [Fact]
        public void EnchantmentRepository_GenerateFromId_ExpectedValues()
        {
            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(true);
            
            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STAT_ID))
                .Returns(COLUMN_INDEX_STAT_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_STAT_ID))
                .Returns(new Guid("c7b34f9c-4bdd-4e1b-ac74-b79d102b9a44"));

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_CALCULATION_ID))
                .Returns(COLUMN_INDEX_CALCULATION_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_CALCULATION_ID))
                .Returns(new Guid("6e497274-bc02-4268-83dc-24ee0123b7ba"));

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_TRIGGER_ID))
                .Returns(COLUMN_INDEX_TRIGGER_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_TRIGGER_ID))
                .Returns(new Guid("d5cfc545-2d99-472a-81ce-9ac62d583a9e"));

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STATUS_TYPE_ID))
                .Returns(COLUMN_INDEX_STATUS_TYPE_ID);
            reader
                .Setup(x => x.GetGuid(COLUMN_INDEX_STATUS_TYPE_ID))
                .Returns(new Guid("5693e7e2-6b11-430c-9f33-80a9389909b4"));

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MAXIMUM_DURATION))
                .Returns(COLUMN_INDEX_MAXIMUM_DURATION);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MAXIMUM_DURATION))
                .Returns(100000);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MINIMUM_DURATION))
                .Returns(COLUMN_INDEX_MINIMUM_DURATION);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MINIMUM_DURATION))
                .Returns(100000);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MAXIMUM_VALUE))
                .Returns(COLUMN_INDEX_MAXIMUM_VALUE);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MAXIMUM_VALUE))
                .Returns(100000);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_MINIMUM_VALUE))
                .Returns(COLUMN_INDEX_MINIMUM_VALUE);
            reader
                .Setup(x => x.GetDouble(COLUMN_INDEX_MINIMUM_VALUE))
                .Returns(100000);
            
            var command = new Mock<IDbCommand>();
            command
                .Setup(x => x.ExecuteReader())
                .Returns(reader.Object);

            var database = new Mock<IDatabase>();
            database
                .Setup(x => x.CreateCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(command.Object);

            var enchantment = new Mock<IEnchantment>();
            enchantment.SetupAllProperties();

            var factory = new Mock<IEnchantmentFactory>();
            factory
                .Setup(x => x.CreateEnchantment())
                .Returns(enchantment.Object);

            var repository = EnchantmentRepository.Create(
                database.Object, 
                factory.Object);
            var guid = new Guid("9a760e46-4a52-416f-8c54-e39b0583610f");
            var rnd = new Mock<IRandom>();

            var result = repository.Generate(guid, rnd.Object);

            Assert.Equal(new Guid("c7b34f9c-4bdd-4e1b-ac74-b79d102b9a44"), result.StatId);
            Assert.Equal(new Guid("6e497274-bc02-4268-83dc-24ee0123b7ba"), result.CalculationId);
            Assert.Equal(new Guid("d5cfc545-2d99-472a-81ce-9ac62d583a9e"), result.TriggerId);
            Assert.Equal(new Guid("5693e7e2-6b11-430c-9f33-80a9389909b4"), result.StatusTypeId);
            Assert.Equal(TimeSpan.FromSeconds(100), result.RemainingDuration);
            Assert.Equal(100000, result.Value);
        }

        [Fact]
        public void EnchantmentRepository_GenerateFromIdNotAvailable_Throws()
        {
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

            var factory = new Mock<IEnchantmentFactory>();

            var repository = EnchantmentRepository.Create(
                database.Object,
                factory.Object);
            var guid = new Guid("9a760e46-4a52-416f-8c54-e39b0583610f");
            var rnd = new Mock<IRandom>();

            var exception = Assert.Throws<InvalidOperationException>(() => repository.Generate(guid, rnd.Object));
            Assert.Equal("Could not spawn enchantment with Id = '" + guid + "'.", exception.Message);
        }
        #endregion
    }
}