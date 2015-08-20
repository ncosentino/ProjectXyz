using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Additive.Sql
{
    public sealed class AdditiveEnchantmentDefinitionRepository : IAdditiveEnchantmentDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IAdditiveEnchantmentDefinitionFactory _factory;
        #endregion

        #region Constructors
        private AdditiveEnchantmentDefinitionRepository(
            IDatabase database,
            IAdditiveEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentDefinitionRepository Create(
            IDatabase database,
            IAdditiveEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentDefinitionRepository>() != null);

            return new AdditiveEnchantmentDefinitionRepository(
                database,
                factory);
        }

        public void Add(IAdditiveEnchantmentDefinition enchantmentDefinition)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentDefinitionId", enchantmentDefinition.Id },
                { "StatId", enchantmentDefinition.Id },
                { "MinimumValue", enchantmentDefinition.MinimumValue },
                { "MaximumValue", enchantmentDefinition.MaximumValue },
                { "MinimumDuration", enchantmentDefinition.MinimumDuration.TotalMilliseconds },
                { "MaximumDuration", enchantmentDefinition.MaximumDuration.TotalMilliseconds },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    AdditiveEnchantments
                (
                    EnchantmentDefinitionId,
                    StatId,
                    MinimumValue,
                    MaximumValue,
                    MinimumDuration,
                    MaximumDuration
                )
                VALUES
                (
                    @EnchantmentDefinitionId,
                    @StatId,
                    @MinimumValue,
                    @MaximumValue,
                    @MinimumDuration,
                    @MaximumDuration
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
                    AdditiveEnchantments
                WHERE
                    EnchantmentId = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IAdditiveEnchantmentDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    AdditiveEnchantments
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
        
        private IAdditiveEnchantmentDefinition EnchantmentFromReader(IDataReader reader, IAdditiveEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentDefinition>() != null);

            return factory.CreateEnchantmentDefinition(
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                reader.GetDouble(reader.GetOrdinal("MinimumValue")),
                reader.GetDouble(reader.GetOrdinal("MaximumValue")),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("MinimumDuration"))),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("MaximumDuration"))));
        }
        #endregion
    }
}
