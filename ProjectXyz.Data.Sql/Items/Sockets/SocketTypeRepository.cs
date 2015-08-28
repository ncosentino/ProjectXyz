using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items.Sockets
{
    public sealed class SocketTypeRepository : ISocketTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly ISocketTypeFactory _factory;
        #endregion

        #region Constructors
        private SocketTypeRepository(
            IDatabase database,
            ISocketTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static ISocketTypeRepository Create(
            IDatabase database,
            ISocketTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketTypeRepository>() != null);

            return new SocketTypeRepository(
                database,
                factory);
        }

        public void Add(ISocketType socketType)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", socketType.Id },
                { "NameStringResourceId", socketType.NameStringResourceId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    SocketTypes
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
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    SocketTypes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public ISocketType GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketTypes
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
                        throw new InvalidOperationException("No item type with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<ISocketType> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketTypes
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

        private ISocketType GetFromReader(IDataReader reader, ISocketTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketType>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("NameStringResourceId")));
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
