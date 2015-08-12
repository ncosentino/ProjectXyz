using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class StatusNegationRepository : IStatusNegationRepository
    {
        #region Fields
        private readonly IDatabase _database;
        #endregion

        #region Constructors
        private StatusNegationRepository(IDatabase database)
        {
            Contract.Requires<ArgumentNullException>(database != null);

            _database = database;
        }
        #endregion

        #region Methods
        public static IStatusNegationRepository Create(IDatabase database)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Ensures(Contract.Result<IStatusNegationRepository>() != null);

            return new StatusNegationRepository(database);
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
                        yield return CreateStatusNegationFromReader(reader);
                    }
                }
            }
        }

        public IStatusNegation GetForStatId(Guid statId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT
                    *
                FROM
                    StatusNegation
                WHERE
                    StatId=@StatId",
                "StatId",
                statId))
            {
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read()
                        ? CreateStatusNegationFromReader(reader)
                        : null;
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
                    return reader.Read()
                        ? CreateStatusNegationFromReader(reader)
                        : null;
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
                    return reader.Read()
                        ? CreateStatusNegationFromReader(reader)
                        : null;
                }
            }
        }

        public void Add(IStatusNegation statusNegation)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", statusNegation.Id },
                { "StatId", statusNegation.StatId},
                { "EnchantmentStatusId", statusNegation.EnchantmentStatusId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    StatusNegation
                (
                    Id,
                    StatId,
                    EnchantmentStatusId
                )
                VALUES
                (
                    @Id,
                    @StatId,
                    @EnchantmentStatusId
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

        private IStatusNegation CreateStatusNegationFromReader(IDataReader reader)
        {
            throw new NotImplementedException("// TODO: implement this");
        }
        #endregion
    }
}
