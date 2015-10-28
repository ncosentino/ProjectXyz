using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Items;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Data.Sql.Items
{
    public sealed class ItemNamePartRepository : IItemNamePartRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IItemNamePartFactory _factory;
        #endregion

        #region Constructors
        private ItemNamePartRepository(
            IDatabase database,
            IItemNamePartFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IItemNamePartRepository Create(
            IDatabase database,
            IItemNamePartFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemNamePartRepository>() != null);

            return new ItemNamePartRepository(
                database,
                factory);
        }

        public IItemNamePart Add(
            Guid id,
            Guid partId,
            Guid nameStringResourceId,
            int order)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "PartId", partId },
                { "NameStringResourceId", nameStringResourceId },
                { "Order", order },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    ItemNameParts
                (
                    Id,
                    PartId,
                    NameStringResourceId,
                    Order
                )
                VALUES
                (
                    @Id,
                    @PartId
                    @NameStringResourceId,
                    @Order
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }

            var itemNamePart = _factory.Create(
                id,
                partId,
                nameStringResourceId,
                order);
            return itemNamePart;
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemNameParts
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public void RemoveByPartId(Guid partId)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    ItemNameParts
                WHERE
                    PartId = @PartId
                ;",
                "@PartId",
                partId))
            {
                command.ExecuteNonQuery();
            }
        }

        public IItemNamePart GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemNameParts
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
                        throw new InvalidOperationException("No item name part with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IItemNamePart> GetByPartId(Guid partId)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemNameParts
                WHERE
                    PartId = @PartId
                LIMIT 1
                ;",
                "@PartId",
                partId))
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

        public IEnumerable<IItemNamePart> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    ItemNameParts
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

        private IItemNamePart GetFromReader(IDataReader reader, IItemNamePartFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IItemNamePart>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
                reader.GetGuid(reader.GetOrdinal("PartId")),
                reader.GetGuid(reader.GetOrdinal("NameStringResourceId")),
                reader.GetInt32(reader.GetOrdinal("Order")));
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
