using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinitionFactory
    {
        #region Methods
        IEnchantmentDefinition CreateEnchantmentDefinition(
            Guid id,
            Guid statId,
            Guid calculationId,
            Guid triggerId,
            Guid statusTypeId,
            double minimumValue,
            double maximumValue,
            TimeSpan minimumDuration,
            TimeSpan maximumDuration);
        #endregion
    }
}
