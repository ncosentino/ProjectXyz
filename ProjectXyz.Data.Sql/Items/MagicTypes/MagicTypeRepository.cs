using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.MagicTypes;

namespace ProjectXyz.Data.Sql.Items.MagicTypes
{
    public sealed class MagicTypeRepository : IMagicTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IMagicTypeFactory _factory;
        #endregion

        #region Constructors
        private MagicTypeRepository(
            IDatabase database,
            IMagicTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IMagicTypeRepository Create(
            IDatabase database,
            IMagicTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMagicTypeRepository>() != null);

            return new MagicTypeRepository(
                database,
                factory);
        }

        public IMagicType Add(
            Guid id,
            Guid nameStringResourceId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "NameStringResourceId", nameStringResourceId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    MagicTypes
                (
                    Id,
                    NameStringResourceId
                )
                VALUES
                (
                    @Id,
                    @NameStringResourceId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var magicType = _factory.Create(
                id,
                nameStringResourceId);
            return magicType;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    MagicTypes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IMagicType GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypes
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
                        throw new InvalidOperationException("No magic type with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IMagicType> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MagicTypes
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

        private IMagicType GetFromReader(IDataReader reader, IMagicTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMagicType>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("NameStringResourceId")));
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
