using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Sql.Resources
{
    public sealed class StringResourceRepository : IStringResourceRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IStringResourceFactory _factory;
        #endregion

        #region Constructors
        private StringResourceRepository(
            IDatabase database,
            IStringResourceFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IStringResourceRepository Create(
            IDatabase database,
            IStringResourceFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStringResourceRepository>() != null);

            return new StringResourceRepository(
                database,
                factory);
        }

        public IStringResource Add(
            Guid id,
            Guid displayLanguageId,
            string value)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "DisplayLanguageId", displayLanguageId },
                { "Value", value},
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    StringResources
                (
                    Id,
                    DisplayLanguageId,
                    Value
                )
                VALUES
                (
                    @Id,
                    @DisplayLanguageId,
                    @Value
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }
            
            var itemStringResource = _factory.Create(
                id,
                displayLanguageId,
                value);
            return itemStringResource;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    StringResources
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IStringResource GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    StringResources
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
                        throw new InvalidOperationException("No string resource with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IStringResource> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    StringResources
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

        private IStringResource GetFromReader(IDataReader reader, IStringResourceFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IStringResource>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("DisplayLanguageId")),
                reader.GetString(reader.GetOrdinal("Value")));
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
