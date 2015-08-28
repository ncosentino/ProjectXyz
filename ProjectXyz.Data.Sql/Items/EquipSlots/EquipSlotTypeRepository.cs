using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.EquipSlots;

namespace ProjectXyz.Data.Sql.Items.EquipSlots
{
    public sealed class EquipSlotTypeRepository : IEquipSlotTypeRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEquipSlotTypeFactory _factory;
        #endregion

        #region Constructors
        private EquipSlotTypeRepository(
            IDatabase database,
            IEquipSlotTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEquipSlotTypeRepository Create(
            IDatabase database,
            IEquipSlotTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEquipSlotTypeRepository>() != null);

            return new EquipSlotTypeRepository(
                database,
                factory);
        }

        public void Add(IEquipSlotType equipSlotType)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", equipSlotType.Id },
                { "NameStringResourceId", equipSlotType.NameStringResourceId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    EquipSlotTypes
                (
                    Id,
                    NameStringResourceId
                )
                VALUES
                (
                    @Id,
                    @NameStringResourceId
                )
                ;",
                namedParameters))
            {
                command.ExecuteNonQuery();
            }
        }

        public void RemoveById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                DELETE FROM
                    EquipSlotTypes
                WHERE
                    Id = @id
                ;",
                "@id",
                id))
            {
                command.ExecuteNonQuery();
            }
        }

        public IEquipSlotType GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    EquipSlotTypes
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
                        throw new InvalidOperationException("No equip slot type with Id '" + id + "' was found.");
                    }

                    return GetFromReader(reader, _factory);
                }
            }
        }

        public IEnumerable<IEquipSlotType> GetAll()
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    EquipSlotTypes
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

        private IEquipSlotType GetFromReader(IDataReader reader, IEquipSlotTypeFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEquipSlotType>() != null);

            return factory.Create(
                reader.GetGuid(reader.GetOrdinal("Id")),
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
