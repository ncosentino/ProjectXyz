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
    public class EnchantmentPluginRepositoryTests : DatabaseTest
    {
        #region Methods
        [Fact]
        public void GetByEnchantmentDefinitionId_IdExists_ExpectedValues()
        {
            // Setup
            const string STORE_CLASS_NAME = "the class name";
            const string DEFINITION_CLASS_NAME = "the class name";
            var enchantmentPluginId = Guid.NewGuid();
            Guid enchantmentTypeId = Guid.NewGuid();

            CreateEnchantmentPlugin(
                enchantmentPluginId,
                enchantmentTypeId,
                STORE_CLASS_NAME,
                DEFINITION_CLASS_NAME);
            
            var enchantmentPluginFactory = EnchantmentPluginFactory.Create();

            var repository = EnchantmentPluginRepository.Create(
                Database,
                enchantmentPluginFactory);

            // Execute
            var result = repository.GetById(enchantmentPluginId);

            // Assert
            Assert.Equal(enchantmentPluginId, result.Id);
            Assert.Equal(enchantmentTypeId, result.EnchantmentTypeId);
            Assert.Equal(STORE_CLASS_NAME, result.StoreRepositoryClassName);
            Assert.Equal(DEFINITION_CLASS_NAME, result.DefinitionRepositoryClassName);
        }

        private void CreateEnchantmentPlugin(Guid enchantmentPluginId, Guid enchantmentTypeId, string storeRepositoryClassName, string definitionRepositoryClassName)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "@Id", enchantmentPluginId },
                { "@EnchantmentTypeId", enchantmentTypeId },
                { "@StoreRepositoryClassName", storeRepositoryClassName },
                { "@DefinitionRepositoryClassName", definitionRepositoryClassName },
            };

            Database.Execute(@"
                INSERT INTO
                    [EnchantmentPlugins]
                (
                    [Id],
                    [EnchantmentTypeId],
                    [StoreRepositoryClassName],
                    [DefinitionRepositoryClassName]
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @StoreRepositoryClassName,
                    @DefinitionRepositoryClassName
                )
                ;",
                namedParameters);
        }
        #endregion
    }
}