using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Sql.Items.Requirements
{
    public sealed class ItemDefinitionItemMiscRequirementsRepository : IItemDefinitionItemMiscRequirementsRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemDefinitionItemMiscRequirementsFactory _factory;
        #endregion

        #region Constructors
        private ItemDefinitionItemMiscRequirementsRepository(
            IDatabase database,
            IItemDefinitionItemMiscRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemDefinitionItemMiscRequirementsRepository Create(
            IDatabase database,
            IItemDefinitionItemMiscRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinitionItemMiscRequirementsRepository>() != null);

            return new ItemDefinitionItemMiscRequirementsRepository(
                database,
                factory);
        }

        public IItemDefinitionItemMiscRequirements Add(
            Guid id,
            Guid itemId,
            Guid statId)
        {
            var itemStatRequirements = _factory.Create(
                id,
                itemId,
                statId);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", itemStatRequirements.Id },
                { "ItemDefinitionId", itemStatRequirements.ItemDefinitionId },
                { "ItemMiscRequirementsId", itemStatRequirements.ItemMiscRequirementsId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemDefinitionItemMiscRequirements
                (
                    Id,
                    ItemDefinitionId,
                    ItemMiscRequirementsId
                )
                VALUES
                (
                    @Id,
                    @ItemDefinitionId,
                    @ItemMiscRequirementsId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            return itemStatRequirements;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemDefinitionItemMiscRequirements
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemDefinitionItemMiscRequirements GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionItemMiscRequirements
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
                        throw new InvalidOperationException("No item definition item misc requirements with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IItemDefinitionItemMiscRequirements GetByItemDefinitionId(Guid itemDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionItemMiscRequirements
                WHERE
                    ItemDefinitionId = @ItemDefinitionId
                LIMIT 1
                ;",
                "@ItemDefinitionId",
                itemDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item definition item misc requirements with item definition Id '" + itemDefinitionId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemDefinitionItemMiscRequirements> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionItemMiscRequirements
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

        private IItemDefinitionItemMiscRequirements GetFromReader(IDataReader reader, IItemDefinitionItemMiscRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinitionItemMiscRequirements>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("ItemMiscRequirementsId")));
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
