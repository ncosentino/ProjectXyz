using System;
using System.Data;
using System.Data.SQLite;
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
    public class EnchantmentDefinitionRepositoryTests : IDisposable
    {
        #region Constants
        private const string COLUMN_NAME_ID = "Id";
        private const string COLUMN_NAME_STAT_ID = "StatId";
        private const string COLUMN_NAME_CALCULATION_ID = "CalculationId";
        private const string COLUMN_NAME_TRIGGER_ID = "TriggerId";
        private const string COLUMN_NAME_STATUS_TYPE_ID = "StatusTypeId";
        private const string COLUMN_NAME_MINIMUM_VALUE_ID = "MinimumValue";
        private const string COLUMN_NAME_MAXIMUM_VALUE_ID = "MaximumValue";
        private const string COLUMN_NAME_MINIMUM_DURATION_ID = "MinimumDuration";
        private const string COLUMN_NAME_MAXIMUM_DURATION_ID = "MaximumDuration";
        #endregion

        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructors
        public EnchantmentDefinitionRepositoryTests()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();

            _database = SqlDatabase.Create(connection, true);

            SqlDatabaseUpgrader.Create().UpgradeDatabase(_database, 0, 1);
        }
        #endregion

        #region Methods
        [Fact]
        public void EnchantmentDefinitionRepository_GetById_ExpectedValues()
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "@Id", Guid.NewGuid() },
                { "@StatId", Guid.NewGuid() },
                { "@CalculationId", Guid.NewGuid() },
                { "@TriggerId", Guid.NewGuid() },
                { "@StatusTypeId", Guid.NewGuid() },
                { "@MinimumValue", 123d },
                { "@MaximumValue", 456d },
                { "@MinimumDuration", 1234d },
                { "@MaximumDuration", 5678d },
            };

            _database.Execute(@"
                INSERT INTO
                    [Enchantments]
                (
                    [Id],
                    [StatId],
                    [CalculationId],
                    [TriggerId],
                    [StatusTypeId],
                    [MinimumValue],
                    [MaximumValue],
                    [MinimumDuration],
                    [MaximumDuration]
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
                )
                ;",
                namedParameters);

            var factory = EnchantmentDefinitionFactory.Create();
            var repository = EnchantmentDefinitionRepository.Create(
                _database,
                factory);

            var result = repository.GetById((Guid)namedParameters["@Id"]);

            Assert.NotNull(result);
            Assert.Equal(namedParameters["@Id"], result.Id);
            Assert.Equal(namedParameters["@StatId"], result.StatId);
            Assert.Equal(namedParameters["@CalculationId"], result.CalculationId);
            Assert.Equal(namedParameters["@TriggerId"], result.TriggerId);
            Assert.Equal(namedParameters["@StatusTypeId"], result.StatusTypeId);
            Assert.Equal(namedParameters["@MinimumValue"], result.MinimumValue);
            Assert.Equal(namedParameters["@MaximumValue"], result.MaximumValue);
            Assert.Equal(namedParameters["@MinimumDuration"], result.MinimumDuration.TotalMilliseconds);
            Assert.Equal(namedParameters["@MaximumDuration"], result.MaximumDuration.TotalMilliseconds);
        }

        [Fact]
        public void EnchantmentDefinitionRepository_GetByIdNotAvailable_Throws()
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "@Id", Guid.NewGuid() },
                { "@StatId", Guid.NewGuid() },
                { "@CalculationId", Guid.NewGuid() },
                { "@TriggerId", Guid.NewGuid() },
                { "@StatusTypeId", Guid.NewGuid() },
                { "@MinimumValue", 123d },
                { "@MaximumValue", 456d },
                { "@MinimumDuration", 1234d },
                { "@MaximumDuration", 5678d },
            };

            _database.Execute(@"
                INSERT INTO
                    [Enchantments]
                (
                    [Id],
                    [StatId],
                    [CalculationId],
                    [TriggerId],
                    [StatusTypeId],
                    [MinimumValue],
                    [MaximumValue],
                    [MinimumDuration],
                    [MaximumDuration]
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
                )
                ;",
                namedParameters);

            var factory = EnchantmentDefinitionFactory.Create();
            var repository = EnchantmentDefinitionRepository.Create(
                _database,
                factory);

            Guid ID_TO_LOOKUP = Guid.NewGuid();
            var exception = Assert.Throws<InvalidOperationException>(() => repository.GetById(ID_TO_LOOKUP));
            Assert.Equal("No enchantment with Id '" + ID_TO_LOOKUP + "' was found.", exception.Message);
        }

        public void Dispose()
        {
            if (_database != null)
            {
                _database.Close();
            }
        }
        #endregion
    }
}