using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentTypeRepository : IEnchantmentTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructors
        private EnchantmentTypeRepository(IDatabase database)
        {
            Contract.Requires<ArgumentNullException>(database != null);

            _database = database;
        }
        #endregion

        #region Methods
        public static IEnchantmentTypeRepository Create(IDatabase database)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Ensures(Contract.Result<IEnchantmentTypeRepository>() != null);

            return new EnchantmentTypeRepository(database);
        }

        public string GetStoreRepositoryClassName(Guid enchantmentDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    StoreRepositoryClassName
                FROM
                    EnchantmentDefinitions
                LEFT OUTER JOIN 
                    EnchantmentTypes
                ON
                    EnchantmentDefinitions.EnchantmentTypeId=EnchantmentTypes.Id
                WHERE
                    EnchantmentDefinitions.Id = @id
                LIMIT 1",
               "id",
               enchantmentDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment definition with Id '" + enchantmentDefinitionId + "' was found.");
                    }

                    var className = reader.GetString(0);
                    return className;
                }
            }
        }

        public string GetDefinitionRepositoryClassName(Guid enchantmentDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    DefinitionRepositoryClassName
                FROM
                    EnchantmentDefinitions
                LEFT OUTER JOIN 
                    EnchantmentTypes
                ON
                    EnchantmentDefinitions.EnchantmentTypeId=EnchantmentTypes.Id
                WHERE
                    EnchantmentDefinitions.Id = @id
                LIMIT 1",
               "id",
               enchantmentDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment definition with Id '" + enchantmentDefinitionId + "' was found.");
                    }

                    var className = reader.GetString(0);
                    return className;
                }
            }
        }
        #endregion
    }
}
