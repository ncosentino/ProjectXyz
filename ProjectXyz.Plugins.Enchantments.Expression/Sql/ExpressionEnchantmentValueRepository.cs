using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression.Sql
{
    public sealed class ExpressionEnchantmentValueRepository : IExpressionEnchantmentValueRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IExpressionEnchantmentValueFactory _factory;
        #endregion

        #region Constructors
        private ExpressionEnchantmentValueRepository(
            IDatabase database,
            IExpressionEnchantmentValueFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentValueRepository Create(
            IDatabase database,
            IExpressionEnchantmentValueFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueRepository>() != null);

            return new ExpressionEnchantmentValueRepository(
                database,
                factory);
        }

        public void Add(IExpressionEnchantmentValue enchantmentValue)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentValue.Id },
                { "ExpressionEnchantmentId", enchantmentValue.ExpressionEnchantmentId },
                { "IdForExpression", enchantmentValue.IdForExpression },
                { "Value", enchantmentValue.Value },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentValues
                (
                    Id,
                    ExpressionEnchantmentId,
                    IdForExpression,
                    Value
                )
                VALUES
                (
                    @Id,
                    @ExpressionEnchantmentId,
                    @IdForExpression,
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
                    ExpressionEnchantmentValues
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
                    ExpressionEnchantmentValues
                WHERE
                    ExpressionEnchantmentId = @ExpressionEnchantmentId
                ;",
                "@ExpressionEnchantmentId",
                expressionEnchantmentId))
            {
                command.ExecuteNonQuery();
            }
        }

        public IExpressionEnchantmentValue GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentValues
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
                        throw new InvalidOperationException("No expression enchantment value with ID '" + id + "' was found.");
                    }

                    return EnchantmentValueFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IExpressionEnchantmentValue> GetByExpressionEnchantmentId(Guid expressionEnchantmentId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentValues
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
                        throw new InvalidOperationException("No expression enchantment value with ExpressionEnchantmentId '" + expressionEnchantmentId + "' was found.");
                    }

                    do
                    {
                        yield return EnchantmentValueFromReader(reader, _factory);
                    }
                    while (reader.Read());
                }
            }
        }
        
        private IExpressionEnchantmentValue EnchantmentValueFromReader(IDataReader reader, IExpressionEnchantmentValueFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValue>() != null);

            return factory.CreateExpressionEnchantmentValue(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ExpressionEnchantmentId")),
                reader.GetString(reader.GetOrdinal("IdForExpression")),
                reader.GetDouble(reader.GetOrdinal("Value")));
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
