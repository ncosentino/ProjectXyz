using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Percentage.Sql
{
    public sealed class PercentageEnchantmentDefinitionRepository : IPercentageEnchantmentDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IPercentageEnchantmentDefinitionFactory _factory;
        #endregion

        #region Constructors
        private PercentageEnchantmentDefinitionRepository(
            IDatabase database,
            IPercentageEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IPercentageEnchantmentDefinitionRepository Create(
            IDatabase database,
            IPercentageEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IPercentageEnchantmentDefinitionRepository>() != null);

            return new PercentageEnchantmentDefinitionRepository(
                database,
                factory);
        }

        public void Add(IPercentageEnchantmentDefinition enchantmentDefinition)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentDefinitionId", enchantmentDefinition.Id },
                { "StatId", enchantmentDefinition.Id },
                { "MinimumValue", enchantmentDefinition.MinimumValue },
                { "MaximumValue", enchantmentDefinition.MaximumValue },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    PercentageEnchantments
                (
                    EnchantmentDefinitionId,
                    StatId,
                    MinimumValue,
                    MaximumValue
                )
                VALUES
                (
                    @EnchantmentDefinitionId,
                    @StatId,
                    @MinimumValue,
                    @MaximumValue
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
                    PercentageEnchantments
                WHERE
                    EnchantmentId = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IPercentageEnchantmentDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    PercentageEnchantments
                WHERE
                    EnchantmentId = @EnchantmentId
                LIMIT 1
                ;",
                "@EnchantmentId",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment with Id '" + id + "' was found.");
                    }

                    return EnchantmentFromReader(reader, _factory);
                }
            }
        }
        
        private IPercentageEnchantmentDefinition EnchantmentFromReader(IDataReader reader, IPercentageEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IPercentageEnchantmentDefinition>() != null);

            return factory.CreateEnchantmentDefinition(
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                reader.GetDouble(reader.GetOrdinal("MinimumValue")),
                reader.GetDouble(reader.GetOrdinal("MaximumValue")));
        }
        #endregion
    }
}
