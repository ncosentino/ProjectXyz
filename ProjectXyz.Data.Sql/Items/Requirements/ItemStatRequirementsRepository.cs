using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Requirements;

namespace ProjectXyz.Data.Sql.Items.Requirements
{
    public sealed class ItemStatRequirementsRepository : IItemStatRequirementsRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemStatRequirementsFactory _factory;
        #endregion

        #region Constructors
        private ItemStatRequirementsRepository(
            IDatabase database,
            IItemStatRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemStatRequirementsRepository Create(
            IDatabase database,
            IItemStatRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemStatRequirementsRepository>() != null);

            return new ItemStatRequirementsRepository(
                database,
                factory);
        }

        public IItemStatRequirements Add(
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
                { "ItemId", itemStatRequirements.ItemId },
                { "StatId", itemStatRequirements.StatId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemStatRequirementss
                (
                    Id,
                    ItemId,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @NameStringResourceId,
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
                    ItemStatRequirementss
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemStatRequirements GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemStatRequirementss
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

        public IItemStatRequirements GetByItemId(Guid itemId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemStatRequirementss
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
                        throw new InvalidOperationException("No item stat requirements with item Id '" + itemId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemStatRequirements> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemStatRequirementss
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

        private IItemStatRequirements GetFromReader(IDataReader reader, IItemStatRequirementsFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemStatRequirements>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemId")),
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
