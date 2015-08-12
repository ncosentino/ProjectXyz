using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.OneShotNegate.Sql
{
    public sealed class OneShotNegateEnchantmentStoreRepository : IEnchantmentStoreRepository<IOneShotNegateEnchantmentStore>
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentStoreRepository<IEnchantmentStore> _enchantmentStoreRepository;
        private readonly IOneShotNegateEnchantmentStoreFactory _factory;
        #endregion

        #region Constructors
        private OneShotNegateEnchantmentStoreRepository(
            IDatabase database,
            IEnchantmentStoreRepository<IEnchantmentStore> enchantmentStoreRepository,
            IOneShotNegateEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreRepository != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _enchantmentStoreRepository = enchantmentStoreRepository;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentStoreRepository<IOneShotNegateEnchantmentStore> Create(
            IDatabase database,
            IEnchantmentStoreRepository<IEnchantmentStore> enchantmentStoreRepository,
            IOneShotNegateEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreRepository != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStoreRepository<IOneShotNegateEnchantmentStore>>() != null);

            return new OneShotNegateEnchantmentStoreRepository(
                database,
                enchantmentStoreRepository,
                factory);
        }

        public void Add(IOneShotNegateEnchantmentStore enchantmentStore)
        {
            _enchantmentStoreRepository.Add(enchantmentStore);

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
            _enchantmentStoreRepository.RemoveById(id);

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
                LEFT OUTER JOIN
                    Enchantments
                ON
                    Enchantments.Id=OneShotNegateEnchantments.EnchantmentId
                WHERE
                    Id = @id
                LIMIT 1
                ;",
                "@id",
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
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return factory.CreateEnchantmentStore(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                reader.GetGuid(reader.GetOrdinal("TriggerId")),
                reader.GetGuid(reader.GetOrdinal("StatusTypeId")));
        }
        #endregion
    }
}
