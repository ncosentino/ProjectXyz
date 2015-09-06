using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression.Sql
{
    public sealed class ExpressionEnchantmentValueDefinitionRepository : IExpressionEnchantmentValueDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IExpressionEnchantmentValueDefinitionFactory _factory;
        #endregion

        #region Constructors
        private ExpressionEnchantmentValueDefinitionRepository(
            IDatabase database,
            IExpressionEnchantmentValueDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentValueDefinitionRepository Create(
            IDatabase database,
            IExpressionEnchantmentValueDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueDefinitionRepository>() != null);

            return new ExpressionEnchantmentValueDefinitionRepository(
                database,
                factory);
        }

        public void Add(IExpressionEnchantmentValueDefinition expressionEnchantmentValueDefinition)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", expressionEnchantmentValueDefinition.Id },
                { "EnchantmentDefinitionId", expressionEnchantmentValueDefinition.EnchantmentDefinitionId },
                { "IdForExpression", expressionEnchantmentValueDefinition.IdForExpression },
                { "MinimumValue", expressionEnchantmentValueDefinition.MinimumValue },
                { "MaximumValue", expressionEnchantmentValueDefinition.MaximumValue },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentValueDefinitions
                (
                    Id,
                    EnchantmentDefinitionId,
                    IdForExpression,
                    MinimumValue,
                    MaximumValue
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
                    @IdForExpression,
                    @MinimumValue,
                    @MaximumValue
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
                    ExpressionEnchantmentValueDefinitions
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IExpressionEnchantmentValueDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentValueDefinitions
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
                        throw new InvalidOperationException("No expression enchantment value definition with ID '" + id + "' was found.");
                    }

                    return ExpressionEnchantmentValueDefinitionFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IExpressionEnchantmentValueDefinition> GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentValueDefinitions
                WHERE
                    EnchantmentDefinitionId = @EnchantmentDefinitionId
                LIMIT 1
                ;",
                "@EnchantmentDefinitionId",
                enchantmentDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No expression enchantment value definition with enchantment definition ID '" + enchantmentDefinitionId + "' was found.");
                    }

                    do
                    {
                        yield return ExpressionEnchantmentValueDefinitionFromReader(reader, _factory);
                    }
                    while (reader.Read());
                }
            }
        }

        private IExpressionEnchantmentValueDefinition ExpressionEnchantmentValueDefinitionFromReader(IDataReader reader, IExpressionEnchantmentValueDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentValueDefinition>() != null);

            return factory.CreateEnchantmentValueDefinition(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")),
                reader.GetString(reader.GetOrdinal("IdForExpression")),
                reader.GetDouble(reader.GetOrdinal("MinimumValue")),
                reader.GetDouble(reader.GetOrdinal("MaximumValue")));
        }
        #endregion
    }
}
