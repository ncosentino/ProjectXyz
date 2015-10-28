using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Data.Sql.Items.Names
{
    public sealed class ItemNameAffixRepository : IItemNameAffixRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemNameAffixFactory _factory;
        #endregion

        #region Constructors
        private ItemNameAffixRepository(
            IDatabase database,
            IItemNameAffixFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemNameAffixRepository Create(
            IDatabase database,
            IItemNameAffixFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemNameAffixRepository>() != null);

            return new ItemNameAffixRepository(
                database,
                factory);
        }

        public IItemNameAffix Add(
            Guid id,
            bool isPrefix,
            Guid itemTypeGroupingId,
            Guid magicTypeGroupingId,
            Guid nameStringResourceId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "IsPrefix", isPrefix },
                { "ItemTypeGroupingId", itemTypeGroupingId },
                { "MagicTypeGroupingId", magicTypeGroupingId },
                { "NameStringResourceId", nameStringResourceId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    RandomItemNameAffixes
                (
                    Id,
                    IsPrefix,
                    ItemTypeGroupingId,
                    MagicTypeGroupingId,
                    NameStringResourceId
                )
                VALUES
                (
                    @Id,
                    @IsPrefix,
                    @ItemTypeGroupingId,
                    @MagicTypeGroupingId,
                    @NameStringResourceId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemNameAffix = _factory.Create(
                id,
                isPrefix,
                itemTypeGroupingId,
                magicTypeGroupingId,
                nameStringResourceId);
            return itemNameAffix;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    RandomItemNameAffixes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemNameAffix GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    RandomItemNameAffixes
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
                        throw new InvalidOperationException("No item name affix with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }
        
        public IEnumerable<IItemNameAffix> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    RandomItemNameAffixes
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

        private IItemNameAffix GetFromReader(IDataReader reader, IItemNameAffixFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemNameAffix>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetBoolean(reader.GetOrdinal("IsPrefix")),
                reader.GetGuid(reader.GetOrdinal("ItemTypeGroupingId")),
                reader.GetGuid(reader.GetOrdinal("MagicTypeGroupingId")),
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
