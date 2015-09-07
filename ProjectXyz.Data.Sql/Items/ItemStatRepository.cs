using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemStatRepository : IItemStatRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemStatFactory _factory;
        #endregion

        #region Constructors
        private ItemStatRepository(
            IDatabase database,
            IItemStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemStatRepository Create(
            IDatabase database,
            IItemStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemStatRepository>() != null);

            return new ItemStatRepository(
                database,
                factory);
        }

        public IItemStat Add(
            Guid id,
            Guid itemId,
            Guid statId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "ItemId", itemId },
                { "StatId", statId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemStats
                (
                    Id,
                    ItemId,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @ItemId,
                    @StatId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }
            
            var itemStat = _factory.Create(
                id,
                itemId,
                statId);
            return itemStat;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemStats
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemStat GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemStats
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
                        throw new InvalidOperationException("No item stat with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemStat> GetByItemId(Guid itemId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemStats
                WHERE
                    ItemId = @ItemId
                ;",
                "@ItemId",
                itemId))
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

        public IEnumerable<IItemStat> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemStats
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

        private IItemStat GetFromReader(IDataReader reader, IItemStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemStat>() != null);

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
