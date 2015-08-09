using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class AdditiveEnchantmentStoreRepository : IEnchantmentStoreRepository<IAdditiveEnchantmentStore>
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentStoreRepository<IEnchantmentStore> _enchantmentStoreRepository;
        private readonly IAdditiveEnchantmentStoreFactory _factory;
        #endregion

        #region Constructors
        private AdditiveEnchantmentStoreRepository(
            IDatabase database,
            IEnchantmentStoreRepository<IEnchantmentStore> enchantmentStoreRepository,
            IAdditiveEnchantmentStoreFactory factory)
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
        public static IEnchantmentStoreRepository<IAdditiveEnchantmentStore> Create(
            IDatabase database,
            IEnchantmentStoreRepository<IEnchantmentStore> enchantmentStoreRepository,
            IAdditiveEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreRepository != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStoreRepository<IAdditiveEnchantmentStore>>() != null);

            return new AdditiveEnchantmentStoreRepository(
                database,
                enchantmentStoreRepository,
                factory);
        }

        public void Add(IAdditiveEnchantmentStore enchantmentStore)
        {
            _enchantmentStoreRepository.Add(enchantmentStore);

            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStore.Id },
                { "StatId", enchantmentStore.StatId },
                { "Value", enchantmentStore.Value },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    AdditiveEnchantments
                (
                    EnchantmentId,
                    StatId,
                    Value
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @Value
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
                LEFT OUTER JOIN
                    Enchantments
                ON
                    Enchantments.Id=AdditiveEnchantments.EnchantmentId
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
        
        private IAdditiveEnchantmentStore EnchantmentFromReader(IDataReader reader, IAdditiveEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return factory.CreateEnchantmentStore(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentTypeId")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                reader.GetGuid(reader.GetOrdinal("TriggerId")),
                reader.GetGuid(reader.GetOrdinal("StatusTypeId")),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("RemainingDuration"))),
                reader.GetDouble(reader.GetOrdinal("Value")));
        }
        #endregion
    }
}
