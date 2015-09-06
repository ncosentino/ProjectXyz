using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemEnchantmentRepository : IItemEnchantmentRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemEnchantmentFactory _factory;
        #endregion

        #region Constructors
        private ItemEnchantmentRepository(
            IDatabase database,
            IItemEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemEnchantmentRepository Create(
            IDatabase database,
            IItemEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemEnchantmentRepository>() != null);

            return new ItemEnchantmentRepository(
                database,
                factory);
        }

        public IItemEnchantment Add(
            Guid id,
            Guid itemId,
            Guid enchantmentId)
        {
            var itemEnchantment = _factory.Create(
                id, 
                itemId,
                enchantmentId);

            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", itemEnchantment.Id },
                { "ItemId", itemEnchantment.ItemId },
                { "EnchantmentId", itemEnchantment.EnchantmentId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemEnchantments
                (
                    Id,
                    ItemId,
                    EnchantmentId
                )
                VALUES
                (
                    @Id,
                    @ItemId,
                    @EnchantmentId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            return itemEnchantment;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemEnchantments
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemEnchantment GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemEnchantments
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
                        throw new InvalidOperationException("No item enchantment with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemEnchantment> GetByItemId(Guid itemId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemEnchantments
                WHERE
                    ItemId = @ItemId
                ;",
                "@ItemId",
                itemId))
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

        public IEnumerable<IItemEnchantment> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemEnchantments
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

        private IItemEnchantment GetFromReader(IDataReader reader, IItemEnchantmentFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemEnchantment>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("ItemId")),
                reader.GetGuid(reader.GetOrdinal("EnchantmentId")));
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
