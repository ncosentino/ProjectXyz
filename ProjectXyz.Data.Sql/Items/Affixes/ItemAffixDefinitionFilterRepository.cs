using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Sql.Items.Affixes
{
    public sealed class ItemAffixDefinitionFilterRepository : IItemAffixDefinitionFilterRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemAffixDefinitionFactory _factory;
        #endregion

        #region Constructors
        private ItemAffixDefinitionFilterRepository(
            IDatabase database,
            IItemAffixDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemAffixDefinitionFilterRepository Create(
            IDatabase database,
            IItemAffixDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionFilterRepository>() != null);

            return new ItemAffixDefinitionFilterRepository(
                database,
                factory);
        }

        public IEnumerable<Guid> GetIdsForFilter(int minimumLevel, int maximumLevel, Guid magicTypeId, bool prefixes, bool suffixes)
        {
            string commandText = @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitions
                LEFT OUTER JOIN
                    ItemAffixDefinitionMagicTypes
                ON
                    ItemAffixDefinitionMagicTypes.ItemAffixDefinitionId = ItemAffixDefinition.Id
                LEFT OUTER JOIN
                    MagicTypeGroupings
                ON
                    MagicTypeGroupings.GroupingId = ItemAffixDefinitionMagicTypes.MagicTypeGroupingsId
                WHERE
                    MinimumLevel >= @MinimumLevel AND
                    MaximumLevel <= @MaximumLevel AND
                    MagicTypeId == @MagicTypeId
                ";

            if (prefixes && !suffixes)
            {
                commandText += " AND IsPrefix";
            } 
            else if (!prefixes && suffixes)
            {
                commandText += " AND NOT IsPrefix";
            }

            commandText += ";";
            
            var namedParameters = new Dictionary<string, object>()
            {
                { "MaximumLevel", maximumLevel},
                { "MinimumLevel", minimumLevel },
                { "MmagicTypeId", magicTypeId },
            };

            using (var command = _database.CreateCommand(commandText, namedParameters))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader.GetGuid(reader.GetOrdinal("Id"));
                    }
                }
            }   
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
