using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Resources.Interface.Languages;

namespace ProjectXyz.Data.Resources.Sql
{
    public sealed class DisplayLanguageRepository : IDisplayLanguageRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IDisplayLanguageFactory _factory;
        #endregion

        #region Constructors
        private DisplayLanguageRepository(
            IDatabase database,
            IDisplayLanguageFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IDisplayLanguageRepository Create(
            IDatabase database,
            IDisplayLanguageFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IDisplayLanguageRepository>() != null);

            return new DisplayLanguageRepository(
                database,
                factory);
        }

        public IDisplayLanguage Add(
            Guid id,
            string name)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "Name", name},
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    DisplayLanguages
                (
                    Id,
                    Name
                )
                VALUES
                (
                    @Id,
                    @Name
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }
            
            var itemDisplayLanguage = _factory.Create(
                id,
                name);
            return itemDisplayLanguage;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    DisplayLanguages
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IDisplayLanguage GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    DisplayLanguages
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
                        throw new InvalidOperationException("No display language with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IDisplayLanguage> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    DisplayLanguages
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

        private IDisplayLanguage GetFromReader(IDataReader reader, IDisplayLanguageFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IDisplayLanguage>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Name")));
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
