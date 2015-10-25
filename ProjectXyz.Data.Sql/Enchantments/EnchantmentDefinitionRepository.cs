using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentDefinitionRepository : IEnchantmentDefinitionRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentDefinitionFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentDefinitionRepository(
            IDatabase database,
            IEnchantmentDefinitionFactory enchantmentDefinitionFactory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(enchantmentDefinitionFactory != null);

            _database = database;
            _factory = enchantmentDefinitionFactory;
        }
        #endregion

        #region Methods
        public static IEnchantmentDefinitionRepository Create(
            IDatabase database,
            IEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentDefinitionRepository>() != null);

            return new EnchantmentDefinitionRepository(
                database,
                factory);
        }

        public IEnchantmentDefinition Add(
            Guid id,
            Guid enchantmentTypeId,
            Guid triggerId,
            Guid statusTypeId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "EnchantmentTypeId", enchantmentTypeId },
                { "TriggerId", triggerId },
                { "StatusTypeId", statusTypeId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    EnchantmentDefinitions
                (
                    Id,
                    EnchantmentTypeId,
                    TriggerId,
                    StatusTypeId
                )
                VALUES
                (
                    @Id,
                    @EnchantmentTypeId,
                    @TriggerId,
                    @StatusTypeId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var enchantmentDefinition = _factory.Create(
                id,
                enchantmentTypeId,
                triggerId,
                statusTypeId);
            return enchantmentDefinition;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    EnchantmentDefinitions
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IEnchantmentDefinition GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    EnchantmentDefinitions
                WHERE
                    Id = @id
                LIMIT 1
                ;",
                "@id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment definition with Id '" + id + "' was found.");
                    }

                    var enchantmentDefinition = EnchantmentFromReader(
                        reader,
                        _factory);
                    return enchantmentDefinition;
                }
            }
        }

        private IEnchantmentDefinition EnchantmentFromReader(IDataReader reader, IEnchantmentDefinitionFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentDefinition>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentTypeId")),
                reader.GetGuid(reader.GetOrdinal("TriggerId")),
                reader.GetGuid(reader.GetOrdinal("StatusTypeId")));
        }
        #endregion
    }
}
