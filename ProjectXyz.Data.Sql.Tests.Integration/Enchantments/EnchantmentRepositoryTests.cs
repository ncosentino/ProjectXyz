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
                { "StatId", "99b946a3-c281-4fcc-8ac6-08d29a4c6c29" },
                { "CalculationId", "de12642d-1b33-4b8b-82e8-7d0f48cd72b1" },
                { "TriggerId", "d5cfc545-2d99-472a-81ce-9ac62d583a9e" },
                { "StatusTypeId", "9fc7d925-8b34-4da1-a73a-a45f8203d19c" },
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

            Assert.Equal(new Guid("99b946a3-c281-4fcc-8ac6-08d29a4c6c29"), result.StatId);
            Assert.Equal(new Guid("de12642d-1b33-4b8b-82e8-7d0f48cd72b1"), result.CalculationId);
            Assert.Equal(new Guid("d5cfc545-2d99-472a-81ce-9ac62d583a9e"), result.TriggerId);
            Assert.Equal(new Guid("9fc7d925-8b34-4da1-a73a-a45f8203d19c"), result.StatusTypeId);
            Assert.Equal(TimeSpan.FromSeconds(100), result.RemainingDuration);
            Assert.Equal(100000, result.Value);
        }
        #endregion
    }
}