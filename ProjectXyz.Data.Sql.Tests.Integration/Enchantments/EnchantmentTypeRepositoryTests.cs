using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Integration.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentTypeRepositoryTests
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructors
        public EnchantmentTypeRepositoryTests()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");
            connection.Open();

            _database = SqlDatabase.Create(connection, true);

            SqlDatabaseUpgrader.Create().UpgradeDatabase(_database, 0, 1);
        }
        #endregion

        #region Methods
        [Fact]
        public void GetStoreRepositoryClassName_IdExists_ExpectedValues()
        {
            // Setup
            Guid enchantmentDefinitionId = Guid.NewGuid();
            const string CLASS_NAME = "the class name";
            var enchantmentTypeId = Guid.NewGuid();

            CreateEnchantmentType(
                enchantmentTypeId,
                CLASS_NAME,
                Guid.NewGuid().ToString());

            CreateEnchantmentDefinition(
                enchantmentDefinitionId, 
                enchantmentTypeId);

            var repository = EnchantmentTypeRepository.Create(_database);

            // Execute
            var result = repository.GetStoreRepositoryClassName(enchantmentDefinitionId);

            // Assert
            Assert.Equal(CLASS_NAME, result);
        }

        [Fact]
        public void GetDefinitionRepositoryClassName_IdExists_ExpectedValues()
        {
            // Setup
            Guid enchantmentDefinitionId = Guid.NewGuid();
            const string CLASS_NAME = "the class name";
            var enchantmentTypeId = Guid.NewGuid();

            CreateEnchantmentType(
                enchantmentTypeId,
                Guid.NewGuid().ToString(),
                CLASS_NAME);

            CreateEnchantmentDefinition(
                enchantmentDefinitionId,
                enchantmentTypeId);

            var repository = EnchantmentTypeRepository.Create(_database);

            // Execute
            var result = repository.GetDefinitionRepositoryClassName(enchantmentDefinitionId);

            // Assert
            Assert.Equal(CLASS_NAME, result);
        }

        private void CreateEnchantmentType(Guid enchantmentTypeId, string storeRepositoryClassName, string definitionRepositoryClassName)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "@Id", enchantmentTypeId },
                { "@StoreRepositoryClassName", storeRepositoryClassName },
                { "@DefinitionRepositoryClassName", definitionRepositoryClassName },
            };

            _database.Execute(@"
                INSERT INTO
                    [EnchantmentTypes]
                (
                    [Id],
                    [StoreRepositoryClassName],
                    [DefinitionRepositoryClassName]
                )
                VALUES
                (
                    @Id,
                    @StoreRepositoryClassName,
                    @DefinitionRepositoryClassName
                )
                ;",
                namedParameters);
        }

        private void CreateEnchantmentDefinition(Guid enchantmentDefinitionId, Guid enchantmentTypeId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "@Id", enchantmentDefinitionId },
                { "@EnchantmentTypeId", enchantmentTypeId },
                { "@TriggerId", Guid.NewGuid().ToString() },
                { "@StatusTypeId", Guid.NewGuid().ToString() },
                { "@MinimumDuration", 0 },
                { "@MaximumDuration", 0 },
            };

            _database.Execute(@"
                INSERT INTO
                    [EnchantmentDefinitions]
                (
                    [Id],
                    [EnchantmentTypeId],
                    [TriggerId],
                    [StatusTypeId],
                    [MinimumDuration],
                    [MaximumDuration]
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @TriggerId,
                    @StatusTypeId,
                    @MinimumDuration,
                    @MaximumDuration
                )
                ;",
                namedParameters);
        }
        #endregion
    }
}