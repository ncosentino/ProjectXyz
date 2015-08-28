using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Sql.Items.Requirements
{
    public sealed class ItemMiscRequirementsRepository : IItemMiscRequirementsRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemMiscRequirementsFactory _factory;
        #endregion

        #region Constructors
        private ItemMiscRequirementsRepository(
            IDatabase database,
            IItemMiscRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemMiscRequirementsRepository Create(
            IDatabase database,
            IItemMiscRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemMiscRequirementsRepository>() != null);

            return new ItemMiscRequirementsRepository(
                database,
                factory);
        }

        public IItemMiscRequirements Add(
            Guid id,
            Guid itemId,
            Guid raceDefinitionId,
            Guid classDefinitionId)
        {
            var itemMiscRequirements = _factory.Create(
                id,
                itemId,
                raceDefinitionId,
                classDefinitionId);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", itemMiscRequirements.Id },
                { "ItemId", itemMiscRequirements.ItemId },
                { "RaceDefinitionId", itemMiscRequirements.RaceDefinitionId },
                { "ClassDefinitionId", itemMiscRequirements.ClassDefinitionId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemMiscRequirementss
                (
                    Id,
                    ItemId,
                    RaceDefinitionId,
                    ClassDefinitionId
                )
                VALUES
                (
                    @Id,
                    @NameStringResourceId,
                    @RaceDefinitionId,
                    @ClassDefinitionId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            return itemMiscRequirements;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemMiscRequirementss
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemMiscRequirements GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemMiscRequirementss
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
                        throw new InvalidOperationException("No item misc requirements with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IItemMiscRequirements GetByItemId(Guid itemId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemMiscRequirementss
                WHERE
                    ItemId = @ItemId
                LIMIT 1
                ;",
                "@ItemId",
                itemId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item misc requirements with item Id '" + itemId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemMiscRequirements> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemMiscRequirementss
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

        private IItemMiscRequirements GetFromReader(IDataReader reader, IItemMiscRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemMiscRequirements>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemId")),
                reader.GetGuid(reader.GetOrdinal("RaceDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("ClassDefinitionId")));
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
