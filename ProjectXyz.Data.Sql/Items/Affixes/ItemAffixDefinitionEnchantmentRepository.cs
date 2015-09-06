using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items.Affixes;

namespace ProjectXyz.Data.Sql.Items.Affixes
{
    public sealed class ItemAffixDefinitionEnchantmentRepository : IItemAffixDefinitionEnchantmentRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemAffixDefinitionEnchantmentFactory _factory;
        #endregion

        #region Constructors
        private ItemAffixDefinitionEnchantmentRepository(
            IDatabase database,
            IItemAffixDefinitionEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemAffixDefinitionEnchantmentRepository Create(
            IDatabase database,
            IItemAffixDefinitionEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionEnchantmentRepository>() != null);

            return new ItemAffixDefinitionEnchantmentRepository(
                database,
                factory);
        }

        public IItemAffixDefinitionEnchantment Add(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid enchantmentDefinitionId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "ItemAffixDefinitionId", itemAffixDefinitionId },
                { "EnchantmentDefinitionId", enchantmentDefinitionId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemAffixDefinitionEnchantments
                (
                    Id,
                    ItemAffixDefinitionId,
                    EnchantmentDefinitionId
                )
                VALUES
                (
                    @Id,
                    @ItemAffixDefinitionId,
                    @EnchantmentDefinitionId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemAffixDefinitionEnchantment = _factory.Create(
                id,
                itemAffixDefinitionId,
                enchantmentDefinitionId);
            return itemAffixDefinitionEnchantment;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemAffixDefinitionEnchantments
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemAffixDefinitionEnchantment GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitionEnchantments
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
                        throw new InvalidOperationException("No item affix definition enchantment with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemAffixDefinitionEnchantment> GetByItemAffixDefinitionId(Guid itemAffixDefinitionId)
        {
            using (var command = _database.CreateCommand(
            @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitionEnchantments
                WHERE
                    ItemAffixDefinitionId = @ItemAffixDefinitionId
                ;",
            "@ItemAffixDefinitionId",
            itemAffixDefinitionId))
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

        public IEnumerable<IItemAffixDefinitionEnchantment> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemAffixDefinitionEnchantments
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
        
        private IItemAffixDefinitionEnchantment GetFromReader(IDataReader reader, IItemAffixDefinitionEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemAffixDefinitionEnchantment>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemAffixDefinitionId")),
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
