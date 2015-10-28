using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Data.Sql.Items.Names
{
    public sealed class ItemNameAffixFilter : IItemNameAffixFilter
    {
        #region Constants

        private const string BASE_FILTER_QUERY = @"
                SELECT 
                    *
                FROM
                    RandomItemNameAffixes
                    JOIN
                        ItemTypeGroupings
                    ON
                        ItemTypeGroupings.GroupingId = RandomItemNameAffixes.ItemTypeGroupingId
                    JOIN
                        MagicTypeGroupings
                    ON
                        MagicTypeGroupings.GroupingId = RandomItemNameAffixes.MagicTypeGroupingId
                WHERE
                    ItemTypeId = @ItemTypeId AND
                    MagicTypeId = @MagicTypeId";
        #endregion

        #region Fields
        private readonly IDatabase _database;
        private readonly IItemNameAffixFactory _factory;
        #endregion

        #region Constructors
        private ItemNameAffixFilter(
            IDatabase database,
            IItemNameAffixFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemNameAffixFilter Create(
            IDatabase database,
            IItemNameAffixFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemNameAffixFilter>() != null);

            return new ItemNameAffixFilter(
                database,
                factory);
        }

        public IItemNameAffix GetRandom(Guid itemTypeId, Guid magicTypeId, bool prefix)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "ItemTypeId", itemTypeId },
                { "MagicTypeId", magicTypeId },
                { "IsPrefix", prefix },
            };

            // FIXME: apparently orderby random() is painfully slow
            // http://stackoverflow.com/a/4740561/2704424
            using (var command = _database.CreateCommand(
                BASE_FILTER_QUERY +
                @"
                AND
                    IsPrefix = @IsPrefix
                ORDER BY 
                    RANDOM() 
                LIMIT 1
                ;",
                namedParameters))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No item name affix with was found that met the specified criteria.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemNameAffix> Filter(Guid itemTypeId, Guid magicTypeId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "ItemTypeId", itemTypeId },
                { "MagicTypeId", magicTypeId },
            };
            
            using (var command = _database.CreateCommand(
                BASE_FILTER_QUERY,
                namedParameters))
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

        private IItemNameAffix GetFromReader(IDataReader reader, IItemNameAffixFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemNameAffix>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetBoolean(reader.GetOrdinal("IsPrefix")),
                reader.GetGuid(reader.GetOrdinal("ItemTypeGroupingId")),
                reader.GetGuid(reader.GetOrdinal("MagicTypeGroupingId")),
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
