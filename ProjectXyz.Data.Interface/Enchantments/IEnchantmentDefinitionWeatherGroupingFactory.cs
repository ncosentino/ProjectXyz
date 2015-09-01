using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinitionWeatherGroupingFactory
    {
        #region Methods
        IEnchantmentDefinitionWeatherGrouping Create(
            Guid id,
            Guid enchantmentDefinitionId,
            Guid weatherTypeGroupingId);
        #endregion
    }
}
