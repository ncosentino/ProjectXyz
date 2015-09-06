using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class StatusNegationRepository : IStatusNegationRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IStatusNegationFactory _factory;
        #endregion

        #region Constructors
        private StatusNegationRepository(
            IDatabase database,
            IStatusNegationFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IStatusNegationRepository Create(
            IDatabase database,
            IStatusNegationFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStatusNegationRepository>() != null);

            var repository = new StatusNegationRepository(
                database,
                factory);
            return repository;
        }

        public IEnumerable<IStatusNegation> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT
                    *
                FROM
                    StatusNegation"))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return CreateStatusNegationFromReader(reader, _factory);
                    }
                }
            }
        }

        public IStatusNegation GetForStatDefinitionId(Guid statDefinitionId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT
                    *
                FROM
                    StatusNegation
                WHERE
                    StatDefinitionId=@StatDefinitionId",
                "StatDefinitionId",
                statDefinitionId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment trigger with stat definition Id '" + statDefinitionId + "' was found.");
                    }

                    var statusNegation = CreateStatusNegationFromReader(reader, _factory);
                    return statusNegation;
                }
            }
        }

        public IStatusNegation GetForId(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT
                    *
                FROM
                    StatusNegation
                WHERE
                    Id=@Id",
                "Id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No status negation with Id '" + id + "' was found.");
                    }

                    var statusNegation = CreateStatusNegationFromReader(reader, _factory);
                    return statusNegation;
                }
            }
        }

        public IStatusNegation GetForEnchantmentStatusId(Guid enchantmentStatusId)
        {
            using (var command = _database.CreateCommand(
              @"
                SELECT
                    *
                FROM
                    StatusNegation
                WHERE
                    EnchantmentStatusId=@EnchantmentStatusId",
              "EnchantmentStatusId",
              enchantmentStatusId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No status negation with enchantment status Id '" + enchantmentStatusId + "' was found.");
                    }

                    var statusNegation = CreateStatusNegationFromReader(reader, _factory);
                    return statusNegation;
                }
            }
        }

        public IStatusNegation Add( 
            Guid id,
            Guid statDefinitionId,
            Guid enchantmentStatusId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "StatDefinitionId", statDefinitionId },
                { "EnchantmentStatusId", enchantmentStatusId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    StatusNegation
                (
                    Id,
                    StatDefinitionId,
                    EnchantmentStatusId
                )
                VALUES
                (
                    @Id,
                    @StatDefinitionId,
                    @EnchantmentStatusId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var statusNegation = _factory.Create(
                id,
                statDefinitionId,
                enchantmentStatusId);
            return statusNegation;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    StatusNegation
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        private IStatusNegation CreateStatusNegationFromReader(IDataReader reader, IStatusNegationFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStatusNegation>() != null);

            var id = reader.GetGuid(reader.GetOrdinal("Id"));
            var statDefinitionId = reader.GetGuid(reader.GetOrdinal("StatDefinitionId"));
            var enchantmentStatusId = reader.GetGuid(reader.GetOrdinal("EnchantmentStatusId"));
            return factory.Create(
                id, 
                statDefinitionId, 
                enchantmentStatusId);
        }

        #endregion
    }
}
