using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemStoreRepository : IItemStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemStoreFactory _factory;
        #endregion

        #region Constructors
        private ItemStoreRepository(
            IDatabase database,
            IItemStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemStoreRepository Create(
            IDatabase database,
            IItemStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemStoreRepository>() != null);

            return new ItemStoreRepository(
                database,
                factory);
        }

        public IItemStore Add(
            Guid id,
            Guid itemDefinitionId,
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
                { "ItemDefinitionId", itemDefinitionId },
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
                    Items
                (
                    Id,
                    ItemDefinitionId,
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
                    @ItemDefinitionId,
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

            var itemStore = _factory.Create(
                id,
                itemDefinitionId,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                itemTypeId,
                materialTypeId,
                socketTypeId);
            return itemStore;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    Items
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemStore GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    Items
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
                        throw new InvalidOperationException("No item with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemStore> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    Items
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

        private IItemStore GetFromReader(IDataReader reader, IItemStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemStore>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemDefinitionId")),
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
