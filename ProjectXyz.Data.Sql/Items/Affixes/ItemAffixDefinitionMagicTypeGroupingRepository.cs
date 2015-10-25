using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Sql.Items.Affixes
{
    public sealed class ItemAffixDefinitionMagicTypeGroupingRepository : IItemAffixDefinitionMagicTypeGroupingRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemAffixDefinitionMagicTypeGroupingFactory _factory;
        #endregion

        #region Constructors
        private ItemAffixDefinitionMagicTypeGroupingRepository(
            IDatabase database,
            IItemAffixDefinitionMagicTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemAffixDefinitionMagicTypeGroupingRepository Create(
            IDatabase database,
            IItemAffixDefinitionMagicTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionMagicTypeGroupingRepository>() != null);

            return new ItemAffixDefinitionMagicTypeGroupingRepository(
                database,
                factory);
        }

        public IItemAffixDefinitionMagicTypeGrouping Add(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid magicTypeGroupingId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "ItemAffixDefinitionId", itemAffixDefinitionId },
                { "MagicTypeGroupingId", magicTypeGroupingId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemAffixDefinitionMagicTypeGrouping
                (
                    Id,
                    ItemAffixDefinitionId,
                    MagicTypeGroupingId
                )
                VALUES
                (
                    @Id,
                    @ItemAffixDefinitionId,
                    @MagicTypeGroupingId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemAffixDefinitionMagicTypeGrouping = _factory.Create(
                id,
                itemAffixDefinitionId,
                magicTypeGroupingId);
            return itemAffixDefinitionMagicTypeGrouping;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemAffixDefinitionMagicTypeGrouping
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemAffixDefinitionMagicTypeGrouping GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitionMagicTypeGrouping
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
                        throw new InvalidOperationException("No item affix definition magic type grouping with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IItemAffixDefinitionMagicTypeGrouping GetByItemAffixDefinitionId(Guid itemAffixDefinitionId)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitionMagicTypeGrouping
                WHERE
                    ItemAffixDefinitionId = @ItemAffixDefinitionId
                ;",
            "@ItemAffixDefinitionId",
            itemAffixDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item affix definition magic type grouping with item affix definition Id '" + itemAffixDefinitionId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemAffixDefinitionMagicTypeGrouping> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitionMagicTypeGrouping
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
        
        private IItemAffixDefinitionMagicTypeGrouping GetFromReader(IDataReader reader, IItemAffixDefinitionMagicTypeGroupingFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionMagicTypeGrouping>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemAffixDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("GroupingId")));
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
