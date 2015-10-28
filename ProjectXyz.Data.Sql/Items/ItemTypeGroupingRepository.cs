using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemTypeGroupingRepository : IItemTypeGroupingRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemTypeGroupingFactory _factory;
        #endregion

        #region Constructors
        private ItemTypeGroupingRepository(
            IDatabase database,
            IItemTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemTypeGroupingRepository Create(
            IDatabase database,
            IItemTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemTypeGroupingRepository>() != null);

            return new ItemTypeGroupingRepository(
                database,
                factory);
        }

        public IItemTypeGrouping Add(
            Guid id,
            Guid groupingId,
            Guid itemTypeId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "GroupingId", groupingId },
                { "ItemTypeId", itemTypeId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemTypeGroupings
                (
                    Id,
                    GroupingId,
                    ItemTypeId
                )
                VALUES
                (
                    @Id,
                    @GroupingId,
                    @ItemTypeId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemTypeGrouping = _factory.Create(
                id,
                groupingId,
                itemTypeId);
            return itemTypeGrouping;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemTypeGroupings
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemTypeGrouping GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemTypeGroupings
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
                        throw new InvalidOperationException("No item type grouping with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemTypeGrouping> GetByGroupingId(Guid groupingId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemTypeGroupings
                WHERE
                    GroupingId = @GroupingId
                LIMIT 1
                ;",
                "@GroupingId",
                groupingId))
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

        public IEnumerable<IItemTypeGrouping> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemTypeGroupings
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

        private IItemTypeGrouping GetFromReader(IDataReader reader, IItemTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemTypeGrouping>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("GroupingId")),
                reader.GetGuid(reader.GetOrdinal("ItemTypeId")));
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
