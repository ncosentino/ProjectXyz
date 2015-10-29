using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentPluginRepository : IEnchantmentPluginRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentPluginFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentPluginRepository(
            IDatabase database,
            IEnchantmentPluginFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentPluginRepository Create(
            IDatabase database,
            IEnchantmentPluginFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentPluginRepository>() != null);

            return new EnchantmentPluginRepository(
                database,
                factory);
        }

        public IEnchantmentPlugin Add(
            Guid id, 
            Guid enchantmentTypeId,
            string storeRepositoryClassName, 
            string definitionRepositoryClassName)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "EnchantmentTypeId", enchantmentTypeId },
                { "StoreRepositoryClassName", storeRepositoryClassName },
                { "DefinitionRepositoryClassName", definitionRepositoryClassName },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    EnchantmentPlugins
                (
                    Id,
                    EnchantmentTypeId,
                    StoreRepositoryClassName,
                    DefinitionRepositoryClassName
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @StoreRepositoryClassName,
                    @DefinitionRepositoryClassName
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var enchantmentPlugin = _factory.Create(
                id,
                enchantmentTypeId,
                storeRepositoryClassName,
                definitionRepositoryClassName);
            return enchantmentPlugin;
        }

        public IEnchantmentPlugin GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    EnchantmentPlugins
                WHERE
                    Id = @Id
                LIMIT 1",
               "Id",
               id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment plugin with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnchantmentPlugin GetByStoreRepositoryClassName(string className)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    EnchantmentPlugins
                WHERE
                    StoreRepositoryClassName = @ClassName
                LIMIT 1",
               "ClassName",
               className))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment plugin with store repository class name '" + className + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnchantmentPlugin GetByDefinitionRepositoryClassName(string className)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    EnchantmentPlugins
                WHERE
                    DefinitionRepositoryClassName = @ClassName
                LIMIT 1",
               "ClassName",
               className))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment plugin with definition repository class name '" + className + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnchantmentPlugin GetByEnchantmentTypeId(Guid enchantmentTypeId)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    EnchantmentPlugins
                WHERE
                    EnchantmentTypeId = @EnchantmentTypeId
                LIMIT 1",
               "EnchantmentTypeId",
               enchantmentTypeId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment plugin with enchantment type Id '" + enchantmentTypeId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IEnchantmentPlugin> GetAll()
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    EnchantmentPlugins
                WHERE
                    Id = @Id"))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return GetFromReader(reader, _factory);   
                    }
                }
            }
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    EnchantmentPlugins
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        private IEnchantmentPlugin GetFromReader(IDataReader reader, IEnchantmentPluginFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentPlugin>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentTypeId")),
                reader.GetString(reader.GetOrdinal("StoreRepositoryClassName")),
                reader.GetString(reader.GetOrdinal("DefinitionRepositoryClassName")));
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_database != null);
            Contract.Invariant(_factory != null);
        }
        #endregion
    }
}
