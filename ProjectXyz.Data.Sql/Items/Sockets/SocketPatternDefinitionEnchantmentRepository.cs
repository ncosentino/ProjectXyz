using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Sockets;

namespace ProjectXyz.Data.Sql.Items.Sockets
{
    public sealed class SocketPatternDefinitionEnchantmentRepository : ISocketPatternDefinitionEnchantmentRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly ISocketPatternEnchantmentFactory _factory;
        #endregion

        #region Constructors
        private SocketPatternDefinitionEnchantmentRepository(
            IDatabase database,
            ISocketPatternEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static ISocketPatternDefinitionEnchantmentRepository Create(
            IDatabase database,
            ISocketPatternEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketPatternDefinitionEnchantmentRepository>() != null);

            return new SocketPatternDefinitionEnchantmentRepository(
                database,
                factory);
        }

        public ISocketPatternEnchantment Add(
            Guid id,
            Guid socketPatternDefinitionId,
            Guid enchantmentDefinitionId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "SocketPatternDefinitionId", socketPatternDefinitionId },
                { "EnchantmentDefinitionId", enchantmentDefinitionId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    SocketPatternDefinitionEnchantments
                (
                    Id,
                    SocketPatternDefinitionId,
                    EnchantmentDefinitionId
                )
                VALUES
                (
                    @Id,
                    @SocketPatternDefinitionId,
                    @EnchantmentDefinitionId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var socketPatternEnchantment = _factory.Create(
                id,
                socketPatternDefinitionId,
                enchantmentDefinitionId);
            return socketPatternEnchantment;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    SocketPatternDefinitionEnchantments
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<ISocketPatternEnchantment> GetBySocketPatternId(Guid socketPatternId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitionEnchantments
                WHERE
                    SocketPatternId = @SocketPatternId
                LIMIT 1
                ;",
                "@SocketPatternId",
                socketPatternId))
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

        public ISocketPatternEnchantment GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitionEnchantments
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
                        throw new InvalidOperationException("No socket pattern definition enchantment with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<ISocketPatternEnchantment> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    SocketPatternDefinitionEnchantments
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

        private ISocketPatternEnchantment GetFromReader(IDataReader reader, ISocketPatternEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<ISocketPatternEnchantment>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("SocketPatternDefinitionId")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentDefinitionId")));
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
