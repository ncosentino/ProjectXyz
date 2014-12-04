using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Core.Enchantments
{
    public class EnchantmentStoreFactory : IEnchantmentStoreFactory
    {
        #region Constructors
        private EnchantmentStoreFactory()
        {
        }
        #endregion

        #region Methods
        public static IEnchantmentStoreFactory Create()
        {
            Contract.Ensures(Contract.Result<IEnchantmentStoreFactory>() != null);

            return new EnchantmentStoreFactory();
        }

        public IEnchantmentStore CreateEnchantmentStore(
            Guid id,
            Guid statId, 
            Guid calculationId, 
            Guid triggerId, 
            Guid statusTypeId, 
            TimeSpan remainingDuration, 
            double value)
        {
            return EnchantmentStore.Create(
                id,
                statId,
                calculationId,
                triggerId,
                statusTypeId,
                remainingDuration,
                value);
        }
        #endregion
    }
}
