﻿using System;
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
                .Setup(x => x.GetString(COLUMN_INDEX_STAT_ID))
                .Returns("Some Stat");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_CALCULATION_ID))
                .Returns(COLUMN_INDEX_CALCULATION_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_CALCULATION_ID))
                .Returns("Some Calculation");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_TRIGGER_ID))
                .Returns(COLUMN_INDEX_TRIGGER_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_TRIGGER_ID))
                .Returns("Some Trigger");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STATUS_TYPE_ID))
                .Returns(COLUMN_INDEX_STATUS_TYPE_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_STATUS_TYPE_ID))
                .Returns("Some Status Type");

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
            var rnd = new Random(123);
            
            var result = repository.Generate(guid, rnd);
            
            Assert.Equal("Some Stat", result.StatId);
            Assert.Equal("Some Calculation", result.CalculationId);
            Assert.Equal("Some Trigger", result.Trigger);
            Assert.Equal("Some Status Type", result.StatusType);
            Assert.Equal(TimeSpan.FromSeconds(100), result.RemainingDuration);
            Assert.Equal(100000, result.Value);
        }

        [Fact]
        public void EnchantmentRepository_GenerateRandomSingleNoAvailable_ThrowsException()
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
            var rnd = new Random(123);

            var exception = Assert.Throws<InvalidOperationException>(() => repository.GenerateRandom(1, 1, 1, rnd).First());
            Assert.Equal("Could not spawn enchantment.", exception.Message);
        }

        [Fact]
        public void EnchantmentRepository_GenerateRandomSingle_SingleValidResult()
        {
            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(true);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STAT_ID))
                .Returns(COLUMN_INDEX_STAT_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_STAT_ID))
                .Returns("Some Stat");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_CALCULATION_ID))
                .Returns(COLUMN_INDEX_CALCULATION_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_CALCULATION_ID))
                .Returns("Some Calculation");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_TRIGGER_ID))
                .Returns(COLUMN_INDEX_TRIGGER_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_TRIGGER_ID))
                .Returns("Some Trigger");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STATUS_TYPE_ID))
                .Returns(COLUMN_INDEX_STATUS_TYPE_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_STATUS_TYPE_ID))
                .Returns("Some Status Type");

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
            var rnd = new Random(123);

            var result = new List<IEnchantment>(repository.GenerateRandom(1, 1, 1, rnd));
            Assert.Equal(1, result.Count);

            Assert.Equal("Some Stat", result[0].StatId);
            Assert.Equal("Some Calculation", result[0].CalculationId);
            Assert.Equal("Some Trigger", result[0].Trigger);
            Assert.Equal("Some Status Type", result[0].StatusType);
            Assert.Equal(TimeSpan.FromSeconds(100), result[0].RemainingDuration);
            Assert.Equal(100000, result[0].Value);
        }

        [Fact]
        public void EnchantmentRepository_GenerateRandomMultiple_MultipleValidResults()
        {
            var reader = new Mock<IDataReader>();
            reader
                .Setup(x => x.Read())
                .Returns(true);

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STAT_ID))
                .Returns(COLUMN_INDEX_STAT_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_STAT_ID))
                .Returns("Some Stat");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_CALCULATION_ID))
                .Returns(COLUMN_INDEX_CALCULATION_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_CALCULATION_ID))
                .Returns("Some Calculation");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_TRIGGER_ID))
                .Returns(COLUMN_INDEX_TRIGGER_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_TRIGGER_ID))
                .Returns("Some Trigger");

            reader
                .Setup(x => x.GetOrdinal(COLUMN_NAME_STATUS_TYPE_ID))
                .Returns(COLUMN_INDEX_STATUS_TYPE_ID);
            reader
                .Setup(x => x.GetString(COLUMN_INDEX_STATUS_TYPE_ID))
                .Returns("Some Status Type");

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
            var rnd = new Random(123);

            var result = new List<IEnchantment>(repository.GenerateRandom(3, 3, 1, rnd));
            Assert.Equal(3, result.Count);

            foreach (var entry in result)
            {
                Assert.Equal("Some Stat", entry.StatId);
                Assert.Equal("Some Calculation", entry.CalculationId);
                Assert.Equal("Some Trigger", entry.Trigger);
                Assert.Equal("Some Status Type", entry.StatusType);
                Assert.Equal(TimeSpan.FromSeconds(100), entry.RemainingDuration);
                Assert.Equal(100000, entry.Value);
            }
        }
        #endregion
    }
}