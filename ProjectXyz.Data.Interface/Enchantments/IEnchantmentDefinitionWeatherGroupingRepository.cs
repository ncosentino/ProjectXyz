using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinitionWeatherTypeGroupingRepository
    {
        #region Methods
        IEnchantmentDefinitionWeatherGrouping Add( 
            Guid id,
            Guid enchantmentDefinitionId,
            Guid weatherTypeGroupingId);

        void RemoveById(Guid id);

        IEnchantmentDefinitionWeatherGrouping GetById(Guid id);

        IEnchantmentDefinitionWeatherGrouping GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId);
        #endregion
    }
}
