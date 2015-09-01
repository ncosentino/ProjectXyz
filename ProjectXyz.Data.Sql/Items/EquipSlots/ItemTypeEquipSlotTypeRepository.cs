using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.EquipSlots;

namespace ProjectXyz.Data.Sql.Items.EquipSlots
{
    public sealed class ItemTypeEquipSlotTypeRepository : IItemTypeEquipSlotTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemTypeEquipSlotTypeFactory _factory;
        #endregion

        #region Constructors
        private ItemTypeEquipSlotTypeRepository(
            IDatabase database,
            IItemTypeEquipSlotTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemTypeEquipSlotTypeRepository Create(
            IDatabase database,
            IItemTypeEquipSlotTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemTypeEquipSlotTypeRepository>() != null);

            return new ItemTypeEquipSlotTypeRepository(
                database,
                factory);
        }

        public IItemTypeEquipSlotType Add(
            Guid id,
            Guid itemTypeId,
            Guid equipSlotType)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "ItemTypeId", itemTypeId },
                { "EquipSlotTypeId", equipSlotType },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemTypeEquipSlotTypes
                (
                    Id,
                    ItemTypeId,
                    EquipSlotTypeId
                )
                VALUES
                (
                    @Id,
                    @ItemTypeId,
                    @EquipSlotTypeId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemTypeEquipSlotType = _factory.Create(
                id,
                itemTypeId,
                equipSlotType);
            return itemTypeEquipSlotType;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemTypeEquipSlotTypes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemTypeEquipSlotType GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemTypeEquipSlotTypes
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
                        throw new InvalidOperationException("No equip slot type with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemTypeEquipSlotType> GetByItemTypeId(Guid itemTypeId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemTypeEquipSlotTypes
                WHERE
                    ItemTypeId = @ItemTypeId
                LIMIT 1
                ;",
                "@ItemTypeId",
                itemTypeId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item type equip slot type with item type Id '" + itemTypeId + "' was found.");
                    }

                    do
                    {
                        yield return GetFromReader(reader, _factory);
                    }
                    while (reader.Read());
                }
            }
        }

        public IEnumerable<IItemTypeEquipSlotType> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemTypeEquipSlotTypes
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

        private IItemTypeEquipSlotType GetFromReader(IDataReader reader, IItemTypeEquipSlotTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemTypeEquipSlotType>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemTypeId")),
                reader.GetGuid(reader.GetOrdinal("EquipSlotTypeId")));
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
