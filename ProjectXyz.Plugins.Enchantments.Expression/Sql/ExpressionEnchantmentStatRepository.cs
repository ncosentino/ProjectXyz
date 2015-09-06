using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression.Sql
{
    public sealed class ExpressionEnchantmentStatRepository : IExpressionEnchantmentStatRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IExpressionEnchantmentStatFactory _factory;
        #endregion

        #region Constructors
        private ExpressionEnchantmentStatRepository(
            IDatabase database,
            IExpressionEnchantmentStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStatRepository Create(
            IDatabase database,
            IExpressionEnchantmentStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatRepository>() != null);

            return new ExpressionEnchantmentStatRepository(
                database,
                factory);
        }

        public void Add(IExpressionEnchantmentStat enchantmentStat)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentStat.Id },
                { "ExpressionEnchantmentId", enchantmentStat.ExpressionEnchantmentId },
                { "IdForExpression", enchantmentStat.IdForExpression },
                { "StatId", enchantmentStat.StatId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentStats
                (
                    Id,
                    ExpressionEnchantmentId,
                    IdForExpression,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @ExpressionEnchantmentId,
                    @IdForExpression,
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
                    ExpressionEnchantmentStats
                WHERE
                    Id = @Id
                ;",
                "@Id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public void RemoveByExpressionEnchantmentId(Guid expressionEnchantmentId)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ExpressionEnchantmentStats
                WHERE
                    ExpressionEnchantmentId = @ExpressionEnchantmentId
                ;",
                "@ExpressionEnchantmentId",
                expressionEnchantmentId))
            {
                command.ExecuteNonQuery();
            }
        }

        public IExpressionEnchantmentStat GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentStats
                WHERE
                    Id = @Id
                LIMIT 1
                ;",
                "@Id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No expression enchantment stat with ID '" + id + "' was found.");
                    }

                    return EnchantmentStatFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IExpressionEnchantmentStat> GetByExpressionEnchantmentId(Guid expressionEnchantmentId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentStats
                WHERE
                    ExpressionEnchantmentId = @ExpressionEnchantmentId
                LIMIT 1
                ;",
                "@ExpressionEnchantmentId",
                expressionEnchantmentId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No expression enchantment stat with ExpressionEnchantmentId '" + expressionEnchantmentId + "' was found.");
                    }

                    do
                    {
                        yield return EnchantmentStatFromReader(reader, _factory);
                    }
                    while (reader.Read());
                }
            }
        }
        
        private IExpressionEnchantmentStat EnchantmentStatFromReader(IDataReader reader, IExpressionEnchantmentStatFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStat>() != null);

            return factory.CreateExpressionEnchantmentStat(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ExpressionEnchantmentId")),
                reader.GetString(reader.GetOrdinal("IdForExpression")),
                reader.GetGuid(reader.GetOrdinal("StatId")));
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
