using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression.Sql
{
    public sealed class ExpressionDefinitionRepository : IExpressionDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IExpressionDefinitionFactory _factory;
        #endregion

        #region Constructors
        private ExpressionDefinitionRepository(
            IDatabase database,
            IExpressionDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IExpressionDefinitionRepository Create(
            IDatabase database,
            IExpressionDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionDefinitionRepository>() != null);

            return new ExpressionDefinitionRepository(
                database,
                factory);
        }

        public void Add(IExpressionDefinition enchantmentDefinition)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentDefinition.Id },
                { "Expression", enchantmentDefinition.Expression },
                { "CalculationPriority", enchantmentDefinition.CalculationPriority },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionDefinitions
                (
                    Id,
                    Expression,
                    CalculationPriority
                )
                VALUES
                (
                    @Id,
                    @Expression,
                    @CalculationPriority
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
                    ExpressionDefinitions
                WHERE
                    Id = @Id
                ;",
                "@Id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }
        
        public IExpressionDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionDefinitions
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
                        throw new InvalidOperationException("No expression definition with ID '" + id + "' was found.");
                    }

                    return ExpressionDefinitionFromReader(reader, _factory);
                }
            }
        }

        private IExpressionDefinition ExpressionDefinitionFromReader(IDataReader reader, IExpressionDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionDefinition>() != null);

            return factory.CreateExpressionDefinition(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Expression")),
                reader.GetInt32(reader.GetOrdinal("CalculationPriority")));
        }
        #endregion
    }
}
