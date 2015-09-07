using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Data.Core.Enchantments;
using ProjectXyz.Data.Sql.Enchantments;
using ProjectXyz.Tests.Integration;
using ProjectXyz.Tests.Xunit.Categories;
using Xunit;

namespace ProjectXyz.Data.Sql.Tests.Integration.Enchantments
{
    [DataLayer]
    [Enchantments]
    public class EnchantmentTypeRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void GetByEnchantmentDefinitionId_IdExists_ExpectedValues()
        {
            // Setup
            Guid enchantmentDefinitionId = Guid.NewGuid();
            const string STORE_CLASS_NAME = "the class name";
            const string DEFINITION_CLASS_NAME = "the class name";
            var enchantmentTypeId = Guid.NewGuid();

            CreateEnchantmentType(
                enchantmentTypeId,
                STORE_CLASS_NAME,
                DEFINITION_CLASS_NAME);

            CreateEnchantmentDefinition(
                enchantmentDefinitionId, 
                enchantmentTypeId);

            var enchantmentTypeFactory = EnchantmentTypeFactory.Create();

            var repository = EnchantmentTypeRepository.Create(
                Database,
                enchantmentTypeFactory);

            // Execute
            var result = repository.GetByEnchantmentDefinitionId(enchantmentDefinitionId);

            // Assert
            Assert.Equal(STORE_CLASS_NAME, result.StoreRepositoryClassName);
            Assert.Equal(DEFINITION_CLASS_NAME, result.DefinitionRepositoryClassName);
        }

        private void CreateEnchantmentType(Guid enchantmentTypeId, string storeRepositoryClassName, string definitionRepositoryClassName)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "@Id", enchantmentTypeId },
                { "@StoreRepositoryClassName", storeRepositoryClassName },
                { "@DefinitionRepositoryClassName", definitionRepositoryClassName },
            };

            Database.Execute(@"
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
            };

            Database.Execute(@"
                INSERT INTO
                    [EnchantmentDefinitions]
                (
                    [Id],
                    [EnchantmentTypeId],
                    [TriggerId],
                    [StatusTypeId]
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @TriggerId,
                    @StatusTypeId
                )
                ;",
                namedParameters);
        }
        #endregion
    }
}