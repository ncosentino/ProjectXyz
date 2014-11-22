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
using ProjectXyz.Data.Core.Enchantments;

namespace ProjectXyz.Data.Sql.Tests.Integration.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentRepositoryTests
    {
        #region Methods
        [Fact]
        public void EnchantmentRepository_GenerateFromId_ExpectedValues()
        {
            var connection = new System.Data.SQLite.SQLiteConnection();
            connection.ConnectionString = "Data Source=:memory:";
            connection.Open();

            var database = SqlDatabase.Create(connection, true);
            var upgrader = SqlDatabaseUpgrader.Create();
            upgrader.UpgradeDatabase(database, 0, 1);

            var enchantmentId = new Guid("9a760e46-4a52-416f-8c54-e39b0583610f");
            var parameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentId },
                { "StatId", "Some Stat" },
                { "CalculationId", "Some Calculation" },
                { "TriggerId", "Some Trigger" },
                { "StatusTypeId", "Some Status Type" },
                { "SpawnLevel", 0 },
                { "MinimumValue", 100000 },
                { "MaximumValue", 100000 },
                { "MinimumDuration", 100000 },
                { "MaximumDuration", 100000 },
            };

            database.Execute(
                @"INSERT INTO Enchantments
                (
                    Id, 
                    StatId, 
                    CalculationId, 
                    TriggerId, 
                    StatusTypeId, 
                    SpawnLevel, 
                    MinimumValue, 
                    MaximumValue, 
                    MinimumDuration, 
                    MaximumDuration
                  )
                  VALUES
                  (
                    @Id,
                    @StatId, 
                    @CalculationId, 
                    @TriggerId, 
                    @StatusTypeId, 
                    @SpawnLevel, 
                    @MinimumValue, 
                    @MaximumValue,
                    @MinimumDuration, 
                    @MaximumDuration
                  )",
                parameters);

            var repository = EnchantmentRepository.Create(
                database,
                EnchantmentFactory.Create());

            var rnd = new Random(123);
            var result = repository.Generate(enchantmentId, rnd);
            
            Assert.Equal("Some Stat", result.StatId);
            Assert.Equal("Some Calculation", result.CalculationId);
            Assert.Equal("Some Trigger", result.Trigger);
            Assert.Equal("Some Status Type", result.StatusType);
            Assert.Equal(TimeSpan.FromSeconds(100), result.RemainingDuration);
            Assert.Equal(100000, result.Value);
        }
        #endregion
    }
}