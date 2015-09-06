using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Materials;

namespace ProjectXyz.Data.Sql.Items.Materials
{
    public sealed class MaterialTypeRepository : IMaterialTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IMaterialTypeFactory _factory;
        #endregion

        #region Constructors
        private MaterialTypeRepository(
            IDatabase database,
            IMaterialTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IMaterialTypeRepository Create(
            IDatabase database,
            IMaterialTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMaterialTypeRepository>() != null);

            return new MaterialTypeRepository(
                database,
                factory);
        }

        public IMaterialType Add(
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
                    MaterialTypes
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

            var materialType = _factory.Create(
                id,
                nameStringResourceId);
            return materialType;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    MaterialTypes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IMaterialType GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MaterialTypes
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
                        throw new InvalidOperationException("No material type with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IMaterialType> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    MaterialTypes
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

        private IMaterialType GetFromReader(IDataReader reader, IMaterialTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IMaterialType>() != null);

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
