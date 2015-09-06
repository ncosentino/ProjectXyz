using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Sql
{
    public sealed class OneShotNegateEnchantmentDefinitionRepository : IOneShotNegateEnchantmentDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IOneShotNegateEnchantmentDefinitionFactory _factory;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentDefinitionRepository(
            IDatabase database,
            IOneShotNegateEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentDefinitionRepository Create(
            IDatabase database,
            IOneShotNegateEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentDefinitionRepository>() != null);

            return new OneShotNegateEnchantmentDefinitionRepository(
                database,
                factory);
        }

        public void Add(IOneShotNegateEnchantmentDefinition enchantmentDefinition)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentDefinitionId", enchantmentDefinition.Id },
                { "StatId", enchantmentDefinition.Id },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    OneShotNegateEnchantments
                (
                    EnchantmentDefinitionId,
                    StatId
                )
                VALUES
                (
                    @EnchantmentDefinitionId,
                    @StatId
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
                    OneShotNegateEnchantments
                WHERE
                    EnchantmentId = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IOneShotNegateEnchantmentDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    OneShotNegateEnchantments
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
        
        private IOneShotNegateEnchantmentDefinition EnchantmentFromReader(IDataReader reader, IOneShotNegateEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentDefinition>() != null);

            return factory.CreateEnchantmentDefinition(
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("StatId")));
        }
        #endregion
    }
}
