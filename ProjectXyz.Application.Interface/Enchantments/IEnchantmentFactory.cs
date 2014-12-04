using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Application.Interface.Enchantments
{
    public interface IEnchantmentFactory
    {
        #region Methods
        IEnchantment Create(IEnchantmentStore enchantmentStore);

        IEnchantment Create(
            Guid id,
            Guid statId, 
            Guid statusTypeId, 
            Guid triggerId, 
            Guid calculationId, 
            double value, 
            TimeSpan duration);
        #endregion
    }
}
