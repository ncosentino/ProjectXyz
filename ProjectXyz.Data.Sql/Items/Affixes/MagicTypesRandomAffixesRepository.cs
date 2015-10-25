using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Sql.Items.Affixes
{
    public sealed class MagicTypesRandomAffixesRepository : IMagicTypesRandomAffixesRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IMagicTypesRandomAffixesFactory _factory;
        #endregion

        #region Constructors
        private MagicTypesRandomAffixesRepository(
            IDatabase database,
            IMagicTypesRandomAffixesFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IMagicTypesRandomAffixesRepository Create(
            IDatabase database,
            IMagicTypesRandomAffixesFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixesRepository>() != null);

            return new MagicTypesRandomAffixesRepository(
                database,
                factory);
        }

        public IMagicTypesRandomAffixes Add(
            Guid id,
            Guid magicTypeId,
            int minimumAffixes,
            int maximumAffixes)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "MagicTypeId", magicTypeId },
                { "MaximumAffixes", minimumAffixes },
                { "MinimumAffixes", maximumAffixes },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    MagicTypesRandomAffixes
                (
                    Id,
                    MagicTypeId,
                    MaximumAffixes,
                    MinimumAffixes
                )
                VALUES
                (
                    @Id,
                    @MagicTypeId,
                    @MaximumAffixes,
                    @MinimumAffixes
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var magicTypesRandomAffixes = _factory.Create(
                id,
                magicTypeId,
                minimumAffixes,
                maximumAffixes);
            return magicTypesRandomAffixes;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    MagicTypesRandomAffixes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IMagicTypesRandomAffixes GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypesRandomAffixes
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
                        throw new InvalidOperationException("No magic types random affixes with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IMagicTypesRandomAffixes GetForMagicTypeId(Guid magicTypeId)
        {
 	         using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypesRandomAffixes
                WHERE
                    MagicTypeId = @MagicTypeId
                LIMIT 1
                ;",
                "@MagicTypeId",
                magicTypeId))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No magic types random affixes with magic type Id '" + magicTypeId + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IMagicTypesRandomAffixes> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypesRandomAffixes
                ;"))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return GetFromReader(reader, _factory);
                    }
                }
            }
        }

        private IMagicTypesRandomAffixes GetFromReader(IDataReader reader, IMagicTypesRandomAffixesFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMagicTypesRandomAffixes>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("MagicTypeId")),
                reader.GetInt32(reader.GetOrdinal("MinimumAffixes")),
                reader.GetInt32(reader.GetOrdinal("MaximumAffixes")));
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
