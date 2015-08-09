using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentStoreRepository : IEnchantmentStoreRepository<IEnchantmentStore>
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructors
        private EnchantmentStoreRepository(IDatabase database)
        {
            Contract.Requires<ArgumentNullException>(database != null);

            _database = database;
        }
        #endregion

        #region Methods
        public static IEnchantmentStoreRepository<IEnchantmentStore> Create(IDatabase database)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Ensures(Contract.Result<IEnchantmentStoreRepository<IEnchantmentStore>>() != null);

            return new EnchantmentStoreRepository(database);
        }

        public void Add(IEnchantmentStore enchantmentStore)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStore.Id },
                { "EnchantmentTypeId", enchantmentStore.EnchantmentTypeId },
                { "TriggerId", enchantmentStore.TriggerId },
                { "StatusTypeId", enchantmentStore.StatusTypeId },
                { "RemainingDuration", enchantmentStore.RemainingDuration.TotalMilliseconds },
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
                    RemainingDuration,
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @TriggerId,
                    @StatusTypeId,
                    @RemainingDuration,
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
            throw new NotSupportedException("Cannot get base enchantments using this repository.");
        }
        #endregion
    }
}
