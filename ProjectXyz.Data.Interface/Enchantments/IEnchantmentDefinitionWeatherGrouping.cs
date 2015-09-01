using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinitionWeatherGrouping
    {
        #region Properties
        Guid Id { get; }

        Guid EnchantmentDefinitionId { get; }

        Guid WeatherTypeGroupingId { get; }
        #endregion
    }
}
