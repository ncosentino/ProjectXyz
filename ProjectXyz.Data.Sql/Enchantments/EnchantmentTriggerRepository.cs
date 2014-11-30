using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentTriggerRepository : IEnchantmentTriggerRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentTriggerFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentTriggerRepository(IDatabase database, IEnchantmentTriggerFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentTriggerRepository Create(IDatabase database, IEnchantmentTriggerFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentTriggerRepository>() != null);

            return new EnchantmentTriggerRepository(database, factory);
        }

        public IEnchantmentTrigger GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    Id,
                    Name
                FROM
                    EnchantmentTriggers
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
                        throw new InvalidOperationException("No enchantment trigger with Id '" + id + "' was found.");
                    }

                    return EnchantmentTriggerFromReader(reader, _factory);
                }
            }
        }

        private IEnchantmentTrigger EnchantmentTriggerFromReader(IDataReader reader, IEnchantmentTriggerFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentTrigger>() != null);

            var id = reader.GetGuid(reader.GetOrdinal("Id"));
            var name = reader.GetString(reader.GetOrdinal("Name"));
            return factory.CreateEnchantmentTrigger(id, name);
        }
        #endregion
    }
}
