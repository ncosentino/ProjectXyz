using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;
using ProjectXyz.Data.Interface;

namespace ProjectXyz.Data.Sql.Enchantments
{
    public sealed class EnchantmentStoreRepository : IEnchantmentStoreRepository
    {
        #region Fields
        private readonly IDatabase _database;
        private readonly IEnchantmentStoreFactory _factory;
        #endregion

        #region Constructors
        private EnchantmentStoreRepository(IDatabase database, IEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);

            _database = database;
            _factory = factory;
        }
        #endregion

        #region Methods
        public static IEnchantmentStoreRepository Create(IDatabase database, IEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(database != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStoreRepository>() != null);

            return new EnchantmentStoreRepository(database, factory);
        }

        public void Add(IEnchantmentStore enchantmentStore)
        {
            throw new NotImplementedException("Implement this functionality.");
        }

        public void RemoveById(Guid id)
        {
            throw new NotImplementedException("Implement this functionality.");
        }

        public IEnchantmentStore GetById(Guid id)
        {
            using (var command = _database.CreateCommand(
                @"
                SELECT 
                    *
                FROM
                    Enchantments
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
                        throw new InvalidOperationException("No enchantment with Id '" + id + "' was found.");
                    }

                    return EnchantmentFromReader(reader, _factory);
                }
            }
        }
        
        private IEnchantmentStore EnchantmentFromReader(IDataReader reader, IEnchantmentStoreFactory factory)
        {
            Contract.Requires<ArgumentNullException>(reader != null);
            Contract.Requires<ArgumentNullException>(factory != null);
            Contract.Ensures(Contract.Result<IEnchantmentStore>() != null);

            throw new NotImplementedException("Implement this functionality.");
        }
        #endregion
    }
}
