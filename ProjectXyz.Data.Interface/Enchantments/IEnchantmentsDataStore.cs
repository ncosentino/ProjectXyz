using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentsDataStore
    {
        #region Properties
        IEnchantmentDefinitionRepository EnchantmentDefinitions { get; }

        IEnchantmentStoreRepository EnchantmentStores { get; }

        IEnchantmentTriggerRepository EnchantmentTriggers { get; }

        IEnchantmentDefinitionWeatherTypeGroupingRepository EnchantmentWeather { get; }

        IStatusNegationRepository StatusNegations { get; }

        IEnchantmentTypeRepository EnchantmentTypes { get; }
        #endregion
    }
}
