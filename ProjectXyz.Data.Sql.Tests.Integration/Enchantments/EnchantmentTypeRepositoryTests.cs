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
            var enchantmentTypeId = Guid.NewGuid();
            var nameStringResourceId = Guid.NewGuid();

            CreateEnchantmentType(
                enchantmentTypeId,
                nameStringResourceId);
            
            var enchantmentTypeFactory = EnchantmentTypeFactory.Create();

            var repository = EnchantmentTypeRepository.Create(
                Database,
                enchantmentTypeFactory);

            // Execute
            var result = repository.GetById(enchantmentTypeId);

            // Assert
            Assert.Equal(enchantmentTypeId, result.Id);
            Assert.Equal(nameStringResourceId, result.NameStringResourceId);
        }

        private void CreateEnchantmentType(Guid enchantmentTypeId, Guid nameStringResourceId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "@Id", enchantmentTypeId },
                { "@NameStringResourceId", nameStringResourceId },
            };

            Database.Execute(@"
                INSERT INTO
                    [EnchantmentTypes]
                (
                    [Id],
                    [NameStringResourceId]
                )
                VALUES
                (
                    @Id,
                    @NameStringResourceId
                )
                ;",
                namedParameters);
        }
        #endregion
    }
}