using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression.Sql
{
    public sealed class ExpressionEnchantmentStoreRepository : IExpressionEnchantmentStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IExpressionEnchantmentStoreFactory _factory;
        #endregion

        #region Constructors
        private ExpressionEnchantmentStoreRepository(
            IDatabase database,
            IExpressionEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStoreRepository Create(
            IDatabase database,
            IExpressionEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStoreRepository>() != null);

            return new ExpressionEnchantmentStoreRepository(
                database,
                factory);
        }

        public void Add(IExpressionEnchantmentStore enchantmentStore)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "EnchantmentId", enchantmentStore.Id },
                { "StatId", enchantmentStore.StatId },
                { "ExpressionId", enchantmentStore.ExpressionId },
                { "RemainingDuration", enchantmentStore.RemainingDuration },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantments
                (
                    EnchantmentId,
                    StatId,
                    ExpressionId,
                    RemainingDuration
                )
                VALUES
                (
                    @EnchantmentId,
                    @StatId,
                    @ExpressionId,
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
                    ExpressionEnchantments
                WHERE
                    EnchantmentId = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IExpressionEnchantmentStore GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantments
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
        
        private IExpressionEnchantmentStore EnchantmentFromReader(IDataReader reader, IExpressionEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStore>() != null);

            return factory.CreateEnchantmentStore(
                reader.GetGuid(reader.GetOrdinal("EnchantmentId")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                reader.GetGuid(reader.GetOrdinal("ExpressionId")),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("RemainingDuration"))));
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(_database != null);
            Contract.Invariant(_factory != null);
        }
        #endregion
    }
}
