using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemDefinitionRepository : IItemDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemDefinitionFactory _factory;
        #endregion

        #region Constructors
        private ItemDefinitionRepository(
            IDatabase database,
            IItemDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemDefinitionRepository Create(
            IDatabase database,
            IItemDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinitionRepository>() != null);

            return new ItemDefinitionRepository(
                database,
                factory);
        }

        public IItemDefinition Add(
            Guid id,
            Guid nameStringResourceId,
            Guid inventoryGraphicResourceId,
            Guid magicTypeId,
            Guid itemTypeId,
            Guid materialTypeId,
            Guid socketTypeId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "InventoryGraphicResourceId", inventoryGraphicResourceId },
                { "ItemTypeId", itemTypeId },
                { "MagicTypeId", magicTypeId },
                { "MaterialTypeId", materialTypeId },
                { "NameStringResourceId", nameStringResourceId },
                { "SocketTypeId", socketTypeId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemDefinitions
                (
                    Id,
                    InventoryGraphicResourceId,
                    ItemTypeId,
                    MagicTypeId,
                    MaterialTypeId,
                    NameStringResourceId,
                    SocketTypeId
                )
                VALUES
                (
                    @Id,
                    @InventoryGraphicResourceId,
                    @ItemTypeId,
                    @MagicTypeId,
                    @MaterialTypeId,
                    @NameStringResourceId,
                    @SocketTypeId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemDefinition = _factory.Create(
                id,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);
            return itemDefinition;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemDefinitions
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitions
                WHERE
                    Id = @Id
                LIMIT 1
                ;",
                "@Id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item definition with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemDefinition> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitions
                ;"))
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

        private IItemDefinition GetFromReader(IDataReader reader, IItemDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinition>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("NameStringResourceId")),
                reader.GetGuid(reader.GetOrdinal("InventoryGraphicResourceId")),
                reader.GetGuid(reader.GetOrdinal("MagicTypeId")),
                reader.GetGuid(reader.GetOrdinal("ItemTypeId")),
                reader.GetGuid(reader.GetOrdinal("MaterialTypeId")),
                reader.GetGuid(reader.GetOrdinal("SocketTypeId")));
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
