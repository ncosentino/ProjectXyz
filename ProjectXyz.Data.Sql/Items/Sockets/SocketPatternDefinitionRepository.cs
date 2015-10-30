using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items.Sockets
{
    public sealed class SocketPatternDefinitionRepository : ISocketPatternDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly ISocketPatternDefinitionFactory _factory;
        #endregion

        #region Constructors
        private SocketPatternDefinitionRepository(
            IDatabase database,
            ISocketPatternDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static ISocketPatternDefinitionRepository Create(
            IDatabase database,
            ISocketPatternDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketPatternDefinitionRepository>() != null);

            return new SocketPatternDefinitionRepository(
                database,
                factory);
        }

        public ISocketPatternDefinition Add(
            Guid id,
            Guid nameStringResourceId,
            Guid? inventoryGraphicResourceId,
            Guid? magicTypeId,
            float chance)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "NameStringResourceId", nameStringResourceId },
                { "InventoryGraphicResourceId", inventoryGraphicResourceId },
                { "MagicTypeId", magicTypeId },
                { "Chance", chance },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    SocketPatternDefinitions
                (
                    Id,
                    SocketPatternDefinitionId,
                    DefinitionId
                )
                VALUES
                (
                    @Id,
                    @SocketPatternDefinitionId,
                    @DefinitionId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var socketPatternDefinition = _factory.Create(
                id,
                nameStringResourceId,
                inventoryGraphicResourceId,
                magicTypeId,
                chance);
            return socketPatternDefinition;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    SocketPatternDefinitions
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }
        
        public ISocketPatternDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitions
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
                        throw new InvalidOperationException("No socket pattern definition with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<ISocketPatternDefinition> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitions
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

        private ISocketPatternDefinition GetFromReader(IDataReader reader, ISocketPatternDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketPatternDefinition>() != null);

            var inventoryGraphicResourceIdIndex = reader.GetOrdinal("InventoryGraphicResourceId");
            var magicTypeIdIndex = reader.GetOrdinal("MagicTypeId");

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("NameStringResourceId")),
                reader.IsDBNull(inventoryGraphicResourceIdIndex) ? null : (Guid?)reader.GetGuid(inventoryGraphicResourceIdIndex),
                reader.IsDBNull(magicTypeIdIndex) ? null : (Guid?)reader.GetGuid(magicTypeIdIndex),
                reader.GetFloat(reader.GetOrdinal("Chance")));
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
