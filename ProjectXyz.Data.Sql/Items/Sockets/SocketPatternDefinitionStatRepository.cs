using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items.Sockets
{
    public sealed class SocketPatternDefinitionStatRepository : ISocketPatternDefinitionStatRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly ISocketPatternStatFactory _factory;
        #endregion

        #region Constructors
        private SocketPatternDefinitionStatRepository(
            IDatabase database,
            ISocketPatternStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static ISocketPatternDefinitionStatRepository Create(
            IDatabase database,
            ISocketPatternStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketPatternDefinitionStatRepository>() != null);

            return new SocketPatternDefinitionStatRepository(
                database,
                factory);
        }

        public ISocketPatternStat Add(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "SocketPatternDefinitionId", socketPatternDefinitionId },
                { "StatDefinitionId", statDefinitionId },
                { "MinimumValue", minimumValue },
                { "MaximumValue", maximumValue },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    SocketPatternDefinitionStats
                (
                    Id,
                    SocketPatternDefinitionId,
                    StatDefinitionId,
                    MinimumValue,
                    MaximumValue
                )
                VALUES
                (
                    @Id,
                    @SocketPatternDefinitionId,
                    @StatDefinitionId,
                    @MinimumValue,
                    @MaximumValue
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var socketPatternStat = _factory.Create(
                id,
                socketPatternDefinitionId,
                statDefinitionId,
                minimumValue,
                maximumValue);
            return socketPatternStat;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    SocketPatternDefinitionStats
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<ISocketPatternStat> GetBySocketPatternId(Guid socketPatternId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitionStats
                WHERE
                    SocketPatternId = @SocketPatternId
                LIMIT 1
                ;",
                "@SocketPatternId",
                socketPatternId))
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

        public ISocketPatternStat GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitionStats
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
                        throw new InvalidOperationException("No socket pattern definition stat with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<ISocketPatternStat> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitionStats
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

        private ISocketPatternStat GetFromReader(IDataReader reader, ISocketPatternStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketPatternStat>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("SocketPatternDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("StatDefinitionId")),
                reader.GetDouble(reader.GetOrdinal("MinimumValue")),
                reader.GetDouble(reader.GetOrdinal("MaximumValue")));
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
