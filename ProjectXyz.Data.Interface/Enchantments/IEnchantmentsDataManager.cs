using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentsDataManager
    {
        #region Properties
        IEnchantmentDefinitionRepository EnchantmentDefinitions { get; }

        IEnchantmentStoreRepository EnchantmentStores { get; }

        IEnchantmentTriggerRepository EnchantmentTriggers { get; }

        IEnchantmentDefinitionWeatherTypeGroupingRepository EnchantmentWeather { get; }

        IStatusNegationRepository StatusNegations { get; }

        IEnchantmentTypeRepository EnchantmentTypes { get; }

        IEnchantmentStoreFactory EnchantmentStoreFactory { get; }

        IEnchantmentStatusRepository EnchantmentStatuses { get; }
        #endregion
    }
}
