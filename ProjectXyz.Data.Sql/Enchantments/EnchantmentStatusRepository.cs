using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics.Contracts;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentStatusRepository : IEnchantmentStatusRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentStatusFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentStatusRepository(IDatabase database, IEnchantmentStatusFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentStatusRepository Create(IDatabase database, IEnchantmentStatusFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStatusRepository>() != null);

            return new EnchantmentStatusRepository(database, factory);
        }

        public IEnchantmentStatus Add(
            Guid id,
            Guid nameStringResourceId)
        {
            var namedParameters = new Dictionary<string, object>()
            {
                { "Id", id },
                { "NameStringResourceId", nameStringResourceId },
            };

            using (var command = _database.CreateCommand(
                @"
                INSERT INTO
                    EnchantmentStatuses
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

            var itemStat = _factory.Create(
                id,
                nameStringResourceId);
            return itemStat;
        }

        public IEnchantmentStatus GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    EnchantmentStatuses
                WHERE
                    Id = @id
                LIMIT 1", 
                "id",
                id))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new InvalidOperationException("No enchantment status with Id '" + id + "' was found.");
                    }

                    return EnchantmentStatusFromReader(reader, _factory);
                }
            }
        }

        private IEnchantmentStatus EnchantmentStatusFromReader(IDataReader reader, IEnchantmentStatusFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStatus>() != null);

            var id = reader.GetGuid(reader.GetOrdinal("Id"));
            var nameStringResourceId = reader.GetGuid(reader.GetOrdinal("NameStringResourceId"));
            return factory.Create(id, nameStringResourceId);
        }
        #endregion
    }
}
