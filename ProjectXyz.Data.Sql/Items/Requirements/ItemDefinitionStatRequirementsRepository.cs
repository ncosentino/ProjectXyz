using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Sql.Items.Requirements
{
    public sealed class ItemDefinitionStatRequirementsRepository : IItemDefinitionStatRequirementsRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemDefinitionStatRequirementsFactory _factory;
        #endregion

        #region Constructors
        private ItemDefinitionStatRequirementsRepository(
            IDatabase database,
            IItemDefinitionStatRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemDefinitionStatRequirementsRepository Create(
            IDatabase database,
            IItemDefinitionStatRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinitionStatRequirementsRepository>() != null);

            return new ItemDefinitionStatRequirementsRepository(
                database,
                factory);
        }

        public IItemDefinitionStatRequirements Add(
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
                { "StatId", itemStatRequirements.StatId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemDefinitionStatRequirements
                (
                    Id,
                    ItemDefinitionId,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @ItemDefinitionId,
                    @StatId
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
                    ItemDefinitionStatRequirements
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemDefinitionStatRequirements GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionStatRequirements
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
                        throw new InvalidOperationException("No item stat requirements with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemDefinitionStatRequirements> GetByItemDefinitionId(Guid itemDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionStatRequirements
                WHERE
                    ItemDefinitionId = @ItemDefinitionId
                LIMIT 1
                ;",
                "@ItemDefinitionId",
                itemDefinitionId))
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

        public IEnumerable<IItemDefinitionStatRequirements> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionStatRequirements
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

        private IItemDefinitionStatRequirements GetFromReader(IDataReader reader, IItemDefinitionStatRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinitionStatRequirements>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("StatId")));
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
