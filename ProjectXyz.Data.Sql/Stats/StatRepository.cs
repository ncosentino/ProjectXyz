using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Stats;

namespace ProjectXyz.Data.Sql.Stats
{
    public sealed class StatRepository : IStatRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IStatFactory _factory;
        #endregion

        #region Constructors
        private StatRepository(
            IDatabase database,
            IStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IStatRepository Create(
            IDatabase database,
            IStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStatRepository>() != null);

            return new StatRepository(
                database,
                factory);
        }

        public IStat Add(
            Guid id,
            Guid statDefinitionId,
            double value)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "StatDefinitionId", statDefinitionId },
                { "Value", value },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    Stats
                (
                    Id,
                    StatDefinitionId,
                    Value
                )
                VALUES
                (
                    @Id,
                    @StatDefinitionId,
                    @Value
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var stat = _factory.Create(
                id,
                statDefinitionId,
                value);

            return stat;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    Stats
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IStat GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    Stats
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
                        throw new InvalidOperationException("No stat with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IStat> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    Stats
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

        private IStat GetFromReader(IDataReader reader, IStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStat>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("StatDefinitionId")),
                reader.GetDouble(reader.GetOrdinal("Value")));
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
