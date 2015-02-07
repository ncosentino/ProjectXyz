using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentStoreRepository : IEnchantmentStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentStoreFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentStoreRepository(IDatabase database, IEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentStoreRepository Create(IDatabase database, IEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStoreRepository>() != null);

            return new EnchantmentStoreRepository(database, factory);
        }

        public void Add(IEnchantmentStore enchantmentStore)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStore.Id },
                { "StatId", enchantmentStore.StatId },
                { "CalculationId", enchantmentStore.CalculationId },
                { "TriggerId", enchantmentStore.TriggerId },
                { "StatusTypeId", enchantmentStore.StatusTypeId },
                { "RemainingDuration", enchantmentStore.RemainingDuration.TotalMilliseconds },
                { "Value", enchantmentStore.Value },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    Enchantments
                (
                    Id,
                    StatId,
                    CalculationId,
                    TriggerId,
                    StatusTypeId,
                    RemainingDuration,
                    Value
                )
                VALUES
                (
                    @Id,
                    @StatId,
                    @CalculationId,
                    @TriggerId,
                    @StatusTypeId,
                    @RemainingDuration,
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

                    return EnchantmentFromReader(reader, _factory);
                }
            }
        }
        
        private IEnchantmentStore EnchantmentFromReader(IDataReader reader, IEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            return factory.CreateEnchantmentStore(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                reader.GetGuid(reader.GetOrdinal("CalculationId")),
                reader.GetGuid(reader.GetOrdinal("TriggerId")),
                reader.GetGuid(reader.GetOrdinal("StatusTypeId")),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("RemainingDuration"))),
                reader.GetDouble(reader.GetOrdinal("Value")));
        }
        #endregion
    }
}
