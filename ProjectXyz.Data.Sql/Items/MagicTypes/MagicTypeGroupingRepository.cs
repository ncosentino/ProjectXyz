using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.MagicTypes;

namespace ProjectXyz.Data.Sql.Items.MagicTypes
{
    public sealed class MagicTypeGroupingRepository : IMagicTypeGroupingRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IMagicTypeGroupingFactory _factory;
        #endregion

        #region Constructors
        private MagicTypeGroupingRepository(
            IDatabase database,
            IMagicTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IMagicTypeGroupingRepository Create(
            IDatabase database,
            IMagicTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMagicTypeGroupingRepository>() != null);

            return new MagicTypeGroupingRepository(
                database,
                factory);
        }

        public IMagicTypeGrouping Add(
            Guid id,
            Guid groupingId,
            Guid magicTypeId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "GroupingId", groupingId },
                { "MagicTypeId", magicTypeId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    MagicTypeGroupings
                (
                    Id,
                    GroupingId,
                    MagicTypeId
                )
                VALUES
                (
                    @Id,
                    @GroupingId,
                    @MagicTypeId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var magicTypeGrouping = _factory.Create(
                id,
                groupingId,
                magicTypeId);
            return magicTypeGrouping;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    MagicTypeGroupings
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IMagicTypeGrouping GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypeGroupings
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
                        throw new InvalidOperationException("No magic type grouping with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IMagicTypeGrouping> GetByGroupingId(Guid groupingId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypeGroupings
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

        public IEnumerable<IMagicTypeGrouping> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypeGroupings
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

        private IMagicTypeGrouping GetFromReader(IDataReader reader, IMagicTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMagicTypeGrouping>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("GroupingId")),
                reader.GetGuid(reader.GetOrdinal("MagicTypeId")));
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
