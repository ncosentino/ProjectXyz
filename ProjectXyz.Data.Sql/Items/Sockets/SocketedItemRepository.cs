using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items.Sockets
{
    public sealed class SocketedItemRepository : ISocketedItemRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly ISocketedItemFactory _factory;
        #endregion

        #region Constructors
        private SocketedItemRepository(
            IDatabase database,
            ISocketedItemFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static ISocketedItemRepository Create(
            IDatabase database,
            ISocketedItemFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketedItemRepository>() != null);

            return new SocketedItemRepository(
                database,
                factory);
        }

        public ISocketedItem Add(
            Guid id,
            Guid parentItemId,
            Guid childItemId)
        {
            var socketedItem = _factory.Create(
                id,
                parentItemId,
                childItemId);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", socketedItem.Id },
                { "ParentItemId", socketedItem.ParentItemId },
                { "ChildItemId", socketedItem.ChildItemId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    SocketedItems
                (
                    Id,
                    NameStringResourceId
                )
                VALUES
                (
                    @Id,
                    @NameStringResourceId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            return socketedItem;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    SocketedItems
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public ISocketedItem GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketedItems
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
                        throw new InvalidOperationException("No socketed item with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<ISocketedItem> GetByParentItemId(Guid parentItemId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketedItems
                WHERE
                    ParentItemId = @ParentItemId
                LIMIT 1
                ;",
                "@ParentItemId",
                parentItemId))
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

        public ISocketedItem GetByChildItemId(Guid childItemId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketedItems
                WHERE
                    ChildItemId = @ChildItemId
                LIMIT 1
                ;",
                "@ChildItemId",
                childItemId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No socketed item with child item Id '" + childItemId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<ISocketedItem> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketedItems
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

        private ISocketedItem GetFromReader(IDataReader reader, ISocketedItemFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketedItem>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ParentItemId")),
                reader.GetGuid(reader.GetOrdinal("ChildItemId")));
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
