using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Sql
{
    public sealed class OneShotNegateEnchantmentStoreRepository : IOneShotNegateEnchantmentStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IOneShotNegateEnchantmentStoreFactory _factory;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentStoreRepository(
            IDatabase database,
            IOneShotNegateEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IOneShotNegateEnchantmentStoreRepository Create(
            IDatabase database,
            IOneShotNegateEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentStoreRepository>() != null);

            return new OneShotNegateEnchantmentStoreRepository(
                database,
                factory);
        }

        public void Add(IOneShotNegateEnchantmentStore enchantmentStore)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStore.Id },
                { "StatId", enchantmentStore.StatId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    OneShotNegateEnchantments
                (
                    EnchantmentId,
                    StatId
                )
                VALUES
                (
                    @EnchantmentId,
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

        public IOneShotNegateEnchantmentStore GetById(Guid id)
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
        
        private IOneShotNegateEnchantmentStore EnchantmentFromReader(IDataReader reader, IOneShotNegateEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IOneShotNegateEnchantmentStore>() != null);

            return factory.CreateEnchantmentStore(
                reader.GetGuid(reader.GetOrdinal("EnchantmentId")),
                reader.GetGuid(reader.GetOrdinal("StatId")));
        }
        #endregion
    }
}
