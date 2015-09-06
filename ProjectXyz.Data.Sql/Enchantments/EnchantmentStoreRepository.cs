using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentStoreRepository : IEnchantmentStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentStoreFactory _enchantmentStoreFactory;
        #endregion

        #region Constructors
        private EnchantmentStoreRepository(
            IDatabase database,
            IEnchantmentStoreFactory enchantmentStoreFactory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);

            _database = database;
            _enchantmentStoreFactory = enchantmentStoreFactory;
        }
        #endregion

        #region Methods
        public static IEnchantmentStoreRepository Create(
                        IDatabase database,
            IEnchantmentStoreFactory enchantmentStoreFactory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(enchantmentStoreFactory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStoreRepository>() != null);

            return new EnchantmentStoreRepository(
                database,
                enchantmentStoreFactory);
        }

        public void Add(IEnchantmentStore enchantmentStore)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStore.Id },
                { "EnchantmentTypeId", enchantmentStore.EnchantmentTypeId },
                { "TriggerId", enchantmentStore.TriggerId },
                { "StatusTypeId", enchantmentStore.StatusTypeId },
                { "EnchantmentWeatherId", enchantmentStore.StatusTypeId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    Enchantments
                (
                    Id,
                    EnchantmentTypeId,
                    TriggerId,
                    StatusTypeId,
                    EnchantmentWeatherId
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @TriggerId,
                    @StatusTypeId,
                    @EnchantmentWeatherId
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
                    Enchantments
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IEnchantmentStore GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    Enchantments
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

                    var enchantmentStore = EnchantmentFromReader(
                        reader,
                        _enchantmentStoreFactory);
                    return enchantmentStore;
                }
            }
        }

        private IEnchantmentStore EnchantmentFromReader(IDataReader reader, IEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("TriggerId")),
                reader.GetGuid(reader.GetOrdinal("StatusTypeId")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentTypeId")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentWeatherId")));
        }
        #endregion
    }
}
