using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemDefinitionStatRepository : IItemDefinitionStatRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemDefinitionStatFactory _factory;
        #endregion

        #region Constructors
        private ItemDefinitionStatRepository(
            IDatabase database,
            IItemDefinitionStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemDefinitionStatRepository Create(
            IDatabase database,
            IItemDefinitionStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinitionStatRepository>() != null);

            return new ItemDefinitionStatRepository(
                database,
                factory);
        }

        public IItemDefinitionStat Add(
            Guid id,
            Guid itemDefinitionId,
            Guid statDefinitionId,
            double minimumValue,
            double maximumValue)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "ItemDefinitionId", itemDefinitionId },
                { "StatDefinitionId", statDefinitionId },
                { "MinimumValue", minimumValue },
                { "MaximumValue", maximumValue },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemDefinitionStats
                (
                    Id,
                    ItemDefinitionId,
                    StatDefinitionId,
                    MinimumValue,
                    MaximumValue
                )
                VALUES
                (
                    @Id,
                    @ItemDefinitionId,
                    @StatDefinitionId,
                    @MinimumValue,
                    @MaximumValue
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemDefinitionStat = _factory.Create(
                id,
                itemDefinitionId,
                statDefinitionId,
                minimumValue,
                maximumValue);
            return itemDefinitionStat;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemDefinitionStats
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemDefinitionStat GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionStats
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
                        throw new InvalidOperationException("No item definition stat with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemDefinitionStat> GetByItemDefinitionId(Guid itemDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionStats
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

        public IEnumerable<IItemDefinitionStat> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemDefinitionStats
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

        private IItemDefinitionStat GetFromReader(IDataReader reader, IItemDefinitionStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemDefinitionStat>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemDefinitionId")),
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
