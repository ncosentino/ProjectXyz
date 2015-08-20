using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Additive.Sql
{
    public sealed class AdditiveEnchantmentStoreRepository : IAdditiveEnchantmentStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IAdditiveEnchantmentStoreFactory _factory;
        #endregion

        #region Constructors
        private AdditiveEnchantmentStoreRepository(
            IDatabase database,
            IAdditiveEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IAdditiveEnchantmentStoreRepository Create(
            IDatabase database,
            IAdditiveEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStoreRepository>() != null);

            return new AdditiveEnchantmentStoreRepository(
                database,
                factory);
        }

        public void Add(IAdditiveEnchantmentStore enchantmentStore)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStore.Id },
                { "StatId", enchantmentStore.StatId },
                { "Value", enchantmentStore.Value },
                { "RemainingDuration", enchantmentStore.RemainingDuration },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    AdditiveEnchantments
                (
                    EnchantmentId,
                    StatId,
                    Value,
                    RemainingDuration
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @Value,
                    @RemainingDuration
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

        public IAdditiveEnchantmentStore GetById(Guid id)
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
        
        private IAdditiveEnchantmentStore EnchantmentFromReader(IDataReader reader, IAdditiveEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IAdditiveEnchantmentStore>() != null);

            return factory.CreateEnchantmentStore(
                reader.GetGuid(reader.GetOrdinal("EnchantmentId")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                reader.GetDouble(reader.GetOrdinal("Value")),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("RemainingDuration"))));
        }
        #endregion
    }
}
