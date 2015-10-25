using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Sql.Items.Affixes
{
    public sealed class ItemAffixDefinitionRepository : IItemAffixDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemAffixDefinitionFactory _factory;
        #endregion

        #region Constructors
        private ItemAffixDefinitionRepository(
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
        public static IItemAffixDefinitionRepository Create(
            IDatabase database,
            IItemAffixDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionRepository>() != null);

            return new ItemAffixDefinitionRepository(
                database,
                factory);
        }

        public IItemAffixDefinition Add(
            Guid id,
            Guid nameStringResourceId,
            bool isPrefix,
            int minimumLevel,
            int maximumLevel)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "IsPrefix", isPrefix },
                { "MaximumLevel", maximumLevel},
                { "MinimumLevel", minimumLevel },
                { "NameStringResourceId", nameStringResourceId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemAffixDefinitions
                (
                    Id,
                    IsPrefix,
                    MaximumLevel,
                    MinimumLevel,
                    NameStringResourceId
                )
                VALUES
                (
                    @Id,
                    @IsPrefix,
                    @MaximumLevel,
                    @MinimumLevel,
                    @NameStringResourceId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemAffixDefinition = _factory.Create(
                id,
                nameStringResourceId,
                isPrefix,
                minimumLevel,
                maximumLevel);
            return itemAffixDefinition;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemAffixDefinitions
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemAffixDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitions
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
                        throw new InvalidOperationException("No item affix definition with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemAffixDefinition> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitions
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
        
        private IItemAffixDefinition GetFromReader(IDataReader reader, IItemAffixDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemAffixDefinition>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("NameStringResourceId")),
                reader.GetBoolean(reader.GetOrdinal("IsPrefix")),
                reader.GetInt32(reader.GetOrdinal("MinimumLevel")),
                reader.GetInt32(reader.GetOrdinal("MaximumLevel")));
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
