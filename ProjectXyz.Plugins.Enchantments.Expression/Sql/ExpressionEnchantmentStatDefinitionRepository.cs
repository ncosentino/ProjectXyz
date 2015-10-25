using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression.Sql
{
    public sealed class ExpressionEnchantmentStatDefinitionRepository : IExpressionEnchantmentStatDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IExpressionEnchantmentStatDefinitionFactory _factory;
        #endregion

        #region Constructors
        private ExpressionEnchantmentStatDefinitionRepository(
            IDatabase database,
            IExpressionEnchantmentStatDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentStatDefinitionRepository Create(
            IDatabase database,
            IExpressionEnchantmentStatDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatDefinitionRepository>() != null);

            return new ExpressionEnchantmentStatDefinitionRepository(
                database,
                factory);
        }

        public void Add(IExpressionEnchantmentStatDefinition expressionEnchantmentStatDefinition)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", expressionEnchantmentStatDefinition.Id },
                { "EnchantmentDefinitionId", expressionEnchantmentStatDefinition.EnchantmentDefinitionId },
                { "IdForExpression", expressionEnchantmentStatDefinition.IdForExpression },
                { "StatId", expressionEnchantmentStatDefinition.StatId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentStatDefinitions
                (
                    Id,
                    EnchantmentDefinitionId,
                    IdForExpression,
                    StatId
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
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
                    ExpressionEnchantmentStatDefinitions
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IExpressionEnchantmentStatDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentStatDefinitions
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
                        throw new InvalidOperationException("No expression enchantment stat definition with ID '" + id + "' was found.");
                    }

                    return ExpressionEnchantmentStatDefinitionFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IExpressionEnchantmentStatDefinition> GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentStatDefinitions
                WHERE
                    EnchantmentDefinitionId = @EnchantmentDefinitionId
                LIMIT 1
                ;",
                "@EnchantmentDefinitionId",
                enchantmentDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return ExpressionEnchantmentStatDefinitionFromReader(reader, _factory);
                    }
                }
            }
        }

        private IExpressionEnchantmentStatDefinition ExpressionEnchantmentStatDefinitionFromReader(IDataReader reader, IExpressionEnchantmentStatDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentStatDefinition>() != null);

            return factory.CreateEnchantmentStatDefinition(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")),
                reader.GetString(reader.GetOrdinal("IdForExpression")),
                reader.GetGuid(reader.GetOrdinal("StatId")));
        }
        #endregion
    }
}
