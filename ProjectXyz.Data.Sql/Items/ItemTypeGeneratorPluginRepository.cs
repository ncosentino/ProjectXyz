using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemTypeGeneratorPluginRepository : IItemTypeGeneratorPluginRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemTypeGeneratorPluginFactory _factory;
        #endregion

        #region Constructors
        private ItemTypeGeneratorPluginRepository(
            IDatabase database,
            IItemTypeGeneratorPluginFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemTypeGeneratorPluginRepository Create(
            IDatabase database,
            IItemTypeGeneratorPluginFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemTypeGeneratorPluginRepository>() != null);

            return new ItemTypeGeneratorPluginRepository(
                database,
                factory);
        }

        public IItemTypeGeneratorPlugin Add(
            Guid id, 
            Guid magicTypeId, 
            string definitionRepositoryClassName)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "MagicTypeId", magicTypeId },
                { "ItemGeneratorClassName", definitionRepositoryClassName },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemTypeGeneratorPlugins
                (
                    Id,
                    MagicTypeId,
                    ItemGeneratorClassName
                )
                VALUES
                (
                    @Id,
                    @MagicTypeId,
                    @ItemGeneratorClassName
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemTypeGeneratorPlugin = _factory.Create(
                id,
                magicTypeId,
                definitionRepositoryClassName);
            return itemTypeGeneratorPlugin;
        }

        public IItemTypeGeneratorPlugin GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    ItemTypeGeneratorPlugins
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
                        throw new InvalidOperationException("No item type generator plugin with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IItemTypeGeneratorPlugin GetByMagicTypeId(Guid magicTypeId)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    ItemTypeGeneratorPlugins
                WHERE
                    MagicTypeId = @MagicTypeId
                LIMIT 1",
               "MagicTypeId",
               magicTypeId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item type generator plugin with magic type Id '" + magicTypeId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IItemTypeGeneratorPlugin GetByItemGeneratorClassName(string itemGeneratorClassName)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    ItemTypeGeneratorPlugins
                WHERE
                    ItemGeneratorClassName = @ItemGeneratorClassName
                LIMIT 1",
               "ItemGeneratorClassName",
               itemGeneratorClassName))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item type generator plugin with item generator class name '" + itemGeneratorClassName + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemTypeGeneratorPlugin> GetAll()
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    ItemTypeGeneratorPlugins
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
                    ItemTypeGeneratorPlugins
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        private IItemTypeGeneratorPlugin GetFromReader(IDataReader reader, IItemTypeGeneratorPluginFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemTypeGeneratorPlugin>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("MagicTypeId")),
                reader.GetString(reader.GetOrdinal("ItemGeneratorClassName")));
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
