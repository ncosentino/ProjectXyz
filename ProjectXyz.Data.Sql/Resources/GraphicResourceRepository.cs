using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Resources;

namespace ProjectXyz.Data.Sql.Resources
{
    public sealed class GraphicResourceRepository : IGraphicResourceRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IGraphicResourceFactory _factory;
        #endregion

        #region Constructors
        private GraphicResourceRepository(
            IDatabase database,
            IGraphicResourceFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IGraphicResourceRepository Create(
            IDatabase database,
            IGraphicResourceFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IGraphicResourceRepository>() != null);

            return new GraphicResourceRepository(
                database,
                factory);
        }

        public IGraphicResource Add(
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
                    GraphicResources
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
            
            var itemGraphicResource = _factory.Create(
                id,
                displayLanguageId,
                value);
            return itemGraphicResource;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    GraphicResources
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IGraphicResource GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    GraphicResources
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

        public IEnumerable<IGraphicResource> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    GraphicResources
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

        private IGraphicResource GetFromReader(IDataReader reader, IGraphicResourceFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IGraphicResource>() != null);

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
