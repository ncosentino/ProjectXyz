using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items.Sockets
{
    public sealed class StatSocketTypeRepository : IStatSocketTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IStatSocketTypeFactory _factory;
        #endregion

        #region Constructors
        private StatSocketTypeRepository(
            IDatabase database,
            IStatSocketTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IStatSocketTypeRepository Create(
            IDatabase database,
            IStatSocketTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStatSocketTypeRepository>() != null);

            return new StatSocketTypeRepository(
                database,
                factory);
        }

        public void Add(IStatSocketType statSocketType)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", statSocketType.Id },
                { "StatDefinitionId", statSocketType.StatDefinitionId },
                { "SocketTypeId", statSocketType.SocketTypeId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    StatSocketTypes
                (
                    Id,
                    StatDefinitionId,
                    SocketTypeId,
                )
                VALUES
                (
                    @Id,
                    @StatDefinitionId,
                    @SocketTypeId
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
                    StatSocketTypes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IStatSocketType GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    StatSocketTypes
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
                        throw new InvalidOperationException("No stat socket type with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IStatSocketType GetByStatDefinitionId(Guid statDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    StatSocketTypes
                WHERE
                    StatDefinitionId = @StatDefinitionId
                LIMIT 1
                ;",
                "@StatDefinitionId",
                statDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No stat socket type with stat definition Id '" + statDefinitionId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IStatSocketType GetBySocketTypeId(Guid socketTypeId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    StatSocketTypes
                WHERE
                    SocketTypeId = @SocketTypeId
                LIMIT 1
                ;",
                "@SocketTypeId",
                socketTypeId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No stat socket type with socket type Id '" + socketTypeId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IStatSocketType> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    StatSocketTypes
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

        private IStatSocketType GetFromReader(IDataReader reader, IStatSocketTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStatSocketType>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("StatDefinitionId")),
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
