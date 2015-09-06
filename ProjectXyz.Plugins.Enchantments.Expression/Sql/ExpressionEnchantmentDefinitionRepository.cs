using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Sql;

namespace ProjectXyz.Plugins.Enchantments.Expression.Sql
{
    public sealed class ExpressionEnchantmentDefinitionRepository : IExpressionEnchantmentDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IExpressionEnchantmentDefinitionFactory _factory;
        #endregion

        #region Constructors
        private ExpressionEnchantmentDefinitionRepository(
            IDatabase database,
            IExpressionEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IExpressionEnchantmentDefinitionRepository Create(
            IDatabase database,
            IExpressionEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentDefinitionRepository>() != null);

            return new ExpressionEnchantmentDefinitionRepository(
                database,
                factory);
        }

        public void Add(IExpressionEnchantmentDefinition enchantmentDefinition)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", enchantmentDefinition.Id },
                { "EnchantmentDefinitionId", enchantmentDefinition.EnchantmentDefinitionId },
                { "ExpressionId", enchantmentDefinition.ExpressionId },
                { "StatId", enchantmentDefinition.StatId },
                { "MinimumDuration", enchantmentDefinition.MinimumDuration.TotalMilliseconds },
                { "MaximumDuration", enchantmentDefinition.MaximumDuration.TotalMilliseconds },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ExpressionEnchantmentDefinitions
                (
                    Id,
                    EnchantmentDefinitionId,
                    ExpressionId,
                    StatId,
                    MinimumDuration,
                    MaximumDuration
                )
                VALUES
                (
                    @Id,
                    @EnchantmentDefinitionId,
                    @ExpressionId,
                    @StatId,
                    @MinimumDuration,
                    @MaximumDuration
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
                    ExpressionEnchantmentDefinitions
                WHERE
                    Id = @Id
                ;",
                "@Id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public void RemoveByEnchantmentDefinitionId(Guid enchantmentDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ExpressionEnchantmentDefinitions
                WHERE
                    EnchantmentDefinitionId = @EnchantmentDefinitionId
                ;",
                "@EnchantmentDefinitionId",
                enchantmentDefinitionId))
            {
                command.ExecuteNonQuery();
            }
        }

        public IExpressionEnchantmentDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentDefinitions
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
                        throw new InvalidOperationException("No expression enchantment definition with ID '" + id + "' was found.");
                    }

                    return ExpressionEnchantmentDefinitionFromReader(reader, _factory);
                }
            }
        }

        public IExpressionEnchantmentDefinition GetByEnchantmentDefinitionId(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ExpressionEnchantmentDefinitions
                WHERE
                    EnchantmentDefinitionId = @EnchantmentDefinitionId
                LIMIT 1
                ;",
                "@EnchantmentDefinitionId",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No expression enchantment definition with enchantment definition ID '" + id + "' was found.");
                    }

                    return ExpressionEnchantmentDefinitionFromReader(reader, _factory);
                }
            }
        }
        
        private IExpressionEnchantmentDefinition ExpressionEnchantmentDefinitionFromReader(IDataReader reader, IExpressionEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IExpressionEnchantmentDefinition>() != null);

            return factory.CreateEnchantmentDefinition(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("ExpressionId")),
                reader.GetGuid(reader.GetOrdinal("StatId")),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("MinimumDuration"))),
                TimeSpan.FromMilliseconds(reader.GetDouble(reader.GetOrdinal("MaximumDuration"))));
        }
        #endregion
    }
}
